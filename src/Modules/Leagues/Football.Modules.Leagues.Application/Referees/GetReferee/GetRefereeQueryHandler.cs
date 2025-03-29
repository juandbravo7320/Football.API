using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Referees.GetReferees;
using Football.Modules.Leagues.Domain.Referees;

namespace Football.Modules.Leagues.Application.Referees.GetReferee;

public class GetRefereeQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetRefereeQuery, RefereeResponse>
{
    public async Task<Result<RefereeResponse>> Handle(GetRefereeQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
                SELECT
                    "Id",
                    "Name",
                    "MinutesPlayed"
                FROM leagues."Referee"
                WHERE "Id" = @RefereeId
             """;

        var referee = await connection.QuerySingleOrDefaultAsync<RefereeResponse>(sql, new { RefereeId = request.Id });
        
        if (referee is null)
            return Result.Failure<RefereeResponse>(RefereeErrors.NotFound(request.Id));

        return Result.Success(referee);
    }
}