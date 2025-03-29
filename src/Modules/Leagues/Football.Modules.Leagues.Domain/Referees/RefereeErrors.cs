using Football.Common.Domain;

namespace Football.Modules.Leagues.Domain.Referees;

public static class RefereeErrors
{
    public static Error NotFound(int refereeId) => 
        Error.NotFound("Referee.NotFound", $"The referee with the identifier {refereeId} was not found");
}