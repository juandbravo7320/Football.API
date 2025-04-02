using System.Data.Common;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Application.Abstractions.Jobs;
using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Domain.MatchPlayers;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.Domain.Referees;
using Microsoft.EntityFrameworkCore;

namespace Football.Modules.Leagues.Application.Matches.CreateMatch;

public class CreateMatchCommandHandler(
    IUnitOfWork unitOfWork,
    IJobScheduler jobScheduler,
    IMatchRepository matchRepository,
    IPlayerRepository playerRepository,
    IRefereeRepository refereeRepository,
    IManagerRepository managerRepository) : ICommandHandler<CreateMatchCommand, int>
{
    public async Task<Result<int>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        
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

        var match = Match.Create(
            request.HouseManager,
            request.AwayManager,
            request.Referee,
            request.StartsAtUtc);

        await matchRepository.Add(match);
        
        var matchPlayers = createMatchPlayers(match, request.HousePlayers, request.AwayPlayers);
        match.MatchPlayers = matchPlayers.ToList();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        
        jobScheduler.ScheduleMatchAlignmentNotification(match.Id, match.StartsAtUtc);

        return Result.Success(match.Id);
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

    private static IEnumerable<MatchPlayer> createMatchPlayers(
        Match match,
        IReadOnlyCollection<int> housePlayers,
        IReadOnlyCollection<int> awayPlayers)
    {
        IEnumerable<MatchPlayer> matchPlayers = new List<MatchPlayer>();
        
        matchPlayers = matchPlayers.Concat(
            housePlayers.Select(hp =>
                MatchPlayer.Create(
                    match.Id, 
                    hp, 
                    PlayerType.Home)));
        
        matchPlayers = matchPlayers.Concat(
            awayPlayers.Select(ap =>
                MatchPlayer.Create(
                    match.Id, 
                    ap, 
                    PlayerType.Away)));

        return matchPlayers;
    }
}