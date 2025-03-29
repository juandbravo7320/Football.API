using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Application.Referees.GetReferees;
using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Domain.MatchPlayers;
using Microsoft.EntityFrameworkCore;

namespace Football.Modules.Leagues.Application.Matches.GetMatch;

public class GetMatchQueryHandler(
    IMatchRepository matchRepository) : IQueryHandler<GetMatchQuery, MatchResponse>
{
    public async Task<Result<MatchResponse>> Handle(GetMatchQuery request, CancellationToken cancellationToken)
    {
        var match = await FetchMatch(request.Id);
        
        if (match is null)
            return Result.Failure<MatchResponse>(
                MatchErrors.NotFound(request.Id));
        
        return Result.Success(match);
    }
    
    private async Task<MatchResponse?> FetchMatch(int matchId)
    {
        return await matchRepository.Queryable()
            .Where(m => m.Id == matchId)
            .Select(m => 
                new MatchResponse(
                    m.Id,
                    new ManagerResponse(
                        m.HouseManager.Id, 
                        m.HouseManager.Name,
                        m.HouseManager.YellowCard,
                        m.HouseManager.RedCard),
                    new ManagerResponse(
                        m.AwayManager.Id, 
                        m.AwayManager.Name,
                        m.AwayManager.YellowCard,
                        m.AwayManager.RedCard),
                    new RefereeResponse(
                        m.Referee.Id, 
                        m.Referee.Name,
                        m.Referee.MinutesPlayed),
                    m.StartsAtUtc,
                    m.MatchPlayers
                        .Where(mp => mp.PlayerType == PlayerType.Home)
                        .Select(mp => 
                            new PlayerResponse(
                                mp.Player.Id, 
                                mp.Player.Name,
                                mp.Player.YellowCard,
                                mp.Player.RedCard,
                                mp.Player.MinutesPlayed))
                        .ToList(),
                    m.MatchPlayers
                        .Where(mp => mp.PlayerType == PlayerType.Away)
                        .Select(mp => 
                            new PlayerResponse(
                                mp.Player.Id, 
                                mp.Player.Name,
                                mp.Player.YellowCard,
                                mp.Player.RedCard,
                                mp.Player.MinutesPlayed))
                        .ToList()
                    ))
            .FirstOrDefaultAsync();
    }
}