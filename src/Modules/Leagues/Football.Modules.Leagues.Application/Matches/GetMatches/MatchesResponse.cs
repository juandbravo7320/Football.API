namespace Football.Modules.Leagues.Application.Matches.GetMatches;

public record MatchesResponse(
    int Id,
    string HomeManager,
    string AwayManager,
    string Referee,
    DateTime StartsAtUtc);