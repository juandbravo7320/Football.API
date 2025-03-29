using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;

namespace Football.Modules.Leagues.Application.Matches.GetMatches;

public class GetMatchesQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetMatchesQuery, IReadOnlyCollection<MatchesResponse>>
{
    public async Task<Result<IReadOnlyCollection<MatchesResponse>>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql =
            $"""
            SELECT
                "match"."Id" AS {nameof(MatchesResponse.Id)},
                "houseManager"."Name" AS {nameof(MatchesResponse.HomeManager)},
                "awayManager"."Name" AS {nameof(MatchesResponse.AwayManager)},
                "referee"."Name" AS {nameof(MatchesResponse.Referee)},
                "match"."StartsAtUtc" AS {nameof(MatchesResponse.StartsAtUtc)}
            FROM leagues."Match" AS "match"
            LEFT JOIN leagues."Manager" AS "houseManager" ON "houseManager"."Id" = "match"."HouseManagerId"
            LEFT JOIN leagues."Manager" AS "awayManager" ON "awayManager"."Id" = "match"."AwayManagerId"
            LEFT JOIN leagues."Referee" AS "referee" ON "referee"."Id" = "match"."RefereeId"
            ORDER BY "match"."StartsAtUtc" DESC
            """;
        
        List<MatchesResponse> matches = (await connection.QueryAsync<MatchesResponse>(sql, request)).AsList();

        return matches;
    }
}