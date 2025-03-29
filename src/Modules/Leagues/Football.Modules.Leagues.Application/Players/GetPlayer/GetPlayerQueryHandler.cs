using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetPlayer;

public class GetPlayerQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetPlayerQuery, PlayerResponse>
{
    public async Task<Result<PlayerResponse>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
                SELECT
                    "Id",
                    "Name",
                    "YellowCard",
                    "RedCard",
                    "MinutesPlayed"
                FROM leagues."Player"
                WHERE "Id" = @PlayerId
             """;

        var player = await connection.QuerySingleOrDefaultAsync<PlayerResponse>(sql, new { PlayerId = request.Id });
        
        if (player is null)
            return Result.Failure<PlayerResponse>(PlayerErrors.NotFound(request.Id));

        return Result.Success(player);
    }
}