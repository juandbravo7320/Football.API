namespace Football.Modules.Leagues.Application.Managers.GetManagers;

public record ManagerResponse(
    int Id,
    string Name,
    int YellowCard,
    int RedCard);