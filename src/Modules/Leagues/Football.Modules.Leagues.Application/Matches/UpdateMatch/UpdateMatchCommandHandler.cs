using System.Data.Common;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Domain.MatchPlayers;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.Domain.Referees;
using Microsoft.EntityFrameworkCore;

namespace Football.Modules.Leagues.Application.Matches.UpdateMatch;

public class UpdateMatchCommandHandler(
    IUnitOfWork unitOfWork,
    IMatchRepository matchRepository,
    IPlayerRepository playerRepository,
    IRefereeRepository refereeRepository,
    IManagerRepository managerRepository,
    IMatchPlayerRepository matchPlayerRepository) : ICommandHandler<UpdateMatchCommand, int>
{
    public async Task<Result<int>> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        
        var match = await GetMatch(request.Id);
        
        if (match is null)
            return Result.Failure<int>(MatchErrors.NotFound(request.Id));
        
        var foundHouseManager = await managerRepository.FindByIdAsync(request.HouseManager);
        
        if (foundHouseManager is null)
            return Result.Failure<int>(ManagerErrors.NotFound(request.HouseManager));
        
        var foundAwayManager = await managerRepository.FindByIdAsync(request.AwayManager);
        
        if (foundAwayManager is null)
            return Result.Failure<int>(ManagerErrors.NotFound(request.AwayManager));
        
        var foundReferee = await refereeRepository.FindByIdAsync(request.Referee);
        
        if (foundReferee is null)
            return Result.Failure<int>(RefereeErrors.NotFound(request.Referee));
        
        var validatePlayersResult = await ValidatePlayers(request.HousePlayers, request.AwayPlayers);
        
        if (validatePlayersResult.IsFailure)
            return Result.Failure<int>(validatePlayersResult.Error);
        
        match.Update(
            request.HouseManager,
            request.AwayManager,
            request.Referee,
            request.StartsAtUtc);
        
        UpdateMatchPlayers(match, request.HousePlayers, request.AwayPlayers);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Result.Success(match.Id);
    }

    private async Task<Match?> GetMatch(int matchId)
    {
        return await matchRepository.Queryable()
            .Include(x => x.MatchPlayers)
            .Where(x => x.Id == matchId)
            .FirstOrDefaultAsync();
    }
    
    private async Task<Result<List<int>>> ValidatePlayers(
        IReadOnlyCollection<int> housePlayers,
        IReadOnlyCollection<int> awayPlayers)
    {
        var foundPlayers = await playerRepository.Queryable()
            .Where(p =>
                housePlayers.Contains(p.Id) ||
                awayPlayers.Contains(p.Id))
            .ToListAsync();

        var players = housePlayers.Concat(awayPlayers);
        var notFoundPlayers = players
            .Except(foundPlayers.Select(p => p.Id))
            .ToList();
        
        if (notFoundPlayers.Count > 0)
            return Result.Failure<List<int>>(PlayerErrors.NotFound(notFoundPlayers));
        
        return Result.Success<List<int>>([]);
    }

    private void UpdateMatchPlayers(
        Match match,
        IReadOnlyCollection<int> housePlayers,
        IReadOnlyCollection<int> awayPlayers)
    {
        var currentMatchPlayers = match.MatchPlayers.ToList();
        
        var housePlayersToAdd = housePlayers
            .Except(currentMatchPlayers.Select(mp => mp.PlayerId))
            .Select(hp => MatchPlayer.Create(match.Id, hp, PlayerType.Home));
        
        var awayPlayersToAdd = awayPlayers
            .Except(currentMatchPlayers.Select(mp => mp.PlayerId))
            .Select(hp => MatchPlayer.Create(match.Id, hp, PlayerType.Away));
        
        var newMatchPlayers = housePlayersToAdd.Concat(awayPlayersToAdd);
        
        var matchPlayersToRemove = currentMatchPlayers
            .Where(mp =>
                !housePlayers.Contains(mp.PlayerId) &&
                !awayPlayers.Contains(mp.PlayerId))
            .ToList();
        
        match.MatchPlayers = newMatchPlayers.ToList();
        matchPlayerRepository.RemoveRange(matchPlayersToRemove);
    }
}