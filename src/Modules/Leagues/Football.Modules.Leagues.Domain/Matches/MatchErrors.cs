using Football.Common.Domain;

namespace Football.Modules.Leagues.Domain.Matches;

public static class MatchErrors
{
    public static Error NotFound(int matchId) =>
        Error.NotFound("Match.NotFound", $"The match with the identifier {matchId} was not found");
}