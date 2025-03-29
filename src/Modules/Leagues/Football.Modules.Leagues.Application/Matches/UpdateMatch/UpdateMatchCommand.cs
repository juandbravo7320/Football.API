using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Matches.UpdateMatch;

public record UpdateMatchCommand(
    int Id,
    int HouseManager,
    int AwayManager,
    int Referee,
    DateTime StartsAtUtc,
    IReadOnlyCollection<int> HousePlayers,
    IReadOnlyCollection<int> AwayPlayers) : ICommand<int>;