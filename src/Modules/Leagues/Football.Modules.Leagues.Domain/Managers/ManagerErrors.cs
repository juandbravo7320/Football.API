using Football.Common.Domain;

namespace Football.Modules.Leagues.Domain.Managers;

public static class ManagerErrors
{
    public static Error NotFound(int managerId) =>
        Error.NotFound("Managers.NotFound", $"The manager with the identifier {managerId} was not found");
}