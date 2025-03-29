using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Managers.GetManagers;

public record GetManagersQuery() : IQuery<IReadOnlyCollection<ManagerResponse>>;