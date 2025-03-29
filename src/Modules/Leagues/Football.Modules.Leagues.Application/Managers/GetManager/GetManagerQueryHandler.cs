using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Domain.Managers;

namespace Football.Modules.Leagues.Application.Managers.GetManager;

public class GetManagerQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetManagerQuery, ManagerResponse>
{
    public async Task<Result<ManagerResponse>> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
                SELECT
                    "Id",
                    "Name",
                    "YellowCard",
                    "RedCard"
                FROM leagues."Manager"
                WHERE "Id" = @ManagerId
             """;

        var manager = await connection.QuerySingleOrDefaultAsync<ManagerResponse>(sql, new { ManagerId = request.Id});
        
        if (manager is null)
            return Result.Failure<ManagerResponse>(ManagerErrors.NotFound(request.Id));

        return Result.Success(manager);
    }
}