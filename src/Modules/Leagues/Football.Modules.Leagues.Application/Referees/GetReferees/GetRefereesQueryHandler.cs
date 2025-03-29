using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;

namespace Football.Modules.Leagues.Application.Referees.GetReferees;

public class GetRefereesQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetRefereesQuery, IReadOnlyCollection<RefereeResponse>>
{
    public async Task<Result<IReadOnlyCollection<RefereeResponse>>> Handle(GetRefereesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(RefereeResponse.Id)},
                 "Name" AS {nameof(RefereeResponse.Name)},
                 "MinutesPlayed" AS {nameof(RefereeResponse.MinutesPlayed)}
             FROM leagues."Referee"
             """;

        var referees = (await connection.QueryAsync<RefereeResponse>(sql, request)).AsList();

        return Result.Success<IReadOnlyCollection<RefereeResponse>>(referees);
    }
}