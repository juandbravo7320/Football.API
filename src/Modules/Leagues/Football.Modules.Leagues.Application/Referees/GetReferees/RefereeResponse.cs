namespace Football.Modules.Leagues.Application.Referees.GetReferees;

public record RefereeResponse(
    int Id,
    string Name,
    int MinutesPlayed);