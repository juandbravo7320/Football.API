using Football.Common.Application.Messaging;
using Football.Modules.Leagues.Application.Managers.GetManagers;

namespace Football.Modules.Leagues.Application.Managers.GetManager;

public record GetManagerQuery(int Id) : IQuery<ManagerResponse>;

