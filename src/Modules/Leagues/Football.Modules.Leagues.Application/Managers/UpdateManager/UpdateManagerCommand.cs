using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Managers.UpdateManager;

public record UpdateManagerCommand(
    int Id,
    string Name,
    int YellowCard,
    int RedCard) : ICommand<int>;