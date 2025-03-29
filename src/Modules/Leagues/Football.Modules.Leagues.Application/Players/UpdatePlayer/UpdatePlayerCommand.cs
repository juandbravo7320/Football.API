using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Players.UpdatePlayer;

public record UpdatePlayerCommand(
    int Id,
    string Name,
    int YellowCard,
    int RedCard,
    int MinutesPlayed) : ICommand<int>;