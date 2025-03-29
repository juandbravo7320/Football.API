using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;

namespace Football.Modules.Leagues.Application.Managers.GetManagers;

public class GetManagersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetManagersQuery, IReadOnlyCollection<ManagerResponse>>
{
    public async Task<Result<IReadOnlyCollection<ManagerResponse>>> Handle(GetManagersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(ManagerResponse.Id)},
                 "Name" AS {nameof(ManagerResponse.Name)},
                 "YellowCard" AS {nameof(ManagerResponse.YellowCard)},
                 "RedCard" AS {nameof(ManagerResponse.RedCard)}
             FROM leagues."Manager"
             """;

        List<ManagerResponse> managers = (await connection.QueryAsync<ManagerResponse>(sql, request)).AsList();

        return managers;
    }
}