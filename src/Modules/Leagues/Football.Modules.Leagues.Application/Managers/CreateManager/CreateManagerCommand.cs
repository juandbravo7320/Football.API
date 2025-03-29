using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Managers.CreateManager;

public record CreateManagerCommand(string Name) : ICommand<int>;