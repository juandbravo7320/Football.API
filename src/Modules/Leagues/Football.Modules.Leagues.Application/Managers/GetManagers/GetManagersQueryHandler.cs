using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Domain.Managers;

namespace Football.Modules.Leagues.Application.Managers.GetManagers;

public class GetManagersQueryHandler(IManagerReadRepository managerReadRepository)
    : IQueryHandler<GetManagersQuery, IReadOnlyCollection<ManagerResponse>>
{
    public async Task<Result<IReadOnlyCollection<ManagerResponse>>> Handle(GetManagersQuery request, CancellationToken cancellationToken)
    {
        const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(ManagerResponse.Id)},
                 "Name" AS {nameof(ManagerResponse.Name)},
                 "YellowCard" AS {nameof(ManagerResponse.YellowCard)},
                 "RedCard" AS {nameof(ManagerResponse.RedCard)}
             FROM leagues."Manager"
             """;

        var managers = await managerReadRepository.QueryAsync(sql, request);

        return managers.Select(m =>
            new ManagerResponse(
                m.Id,
                m.Name,
                m.YellowCard,
                m.RedCard))
            .ToList();
    }
}