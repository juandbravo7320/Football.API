using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Application.Referees.GetReferees;

namespace Football.Modules.Leagues.Application.Matches.GetMatch;

public record MatchResponse(
    int Id,
    ManagerResponse HouseManager,
    ManagerResponse AwayManager,
    RefereeResponse RefereeResponse,
    DateTime StartsAtUtc,
    IReadOnlyCollection<PlayerResponse> HousePlayers,
    IReadOnlyCollection<PlayerResponse> AwayPlayers);