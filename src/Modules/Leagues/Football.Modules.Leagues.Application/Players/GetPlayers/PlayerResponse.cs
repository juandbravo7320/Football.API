namespace Football.Modules.Leagues.Application.Players.GetPlayers;

public record PlayerResponse(
    int Id,
    string Name,
    int YellowCard,
    int RedCard,
    int MinutesPlayed);