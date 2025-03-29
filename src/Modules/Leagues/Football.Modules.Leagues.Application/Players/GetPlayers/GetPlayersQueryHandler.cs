using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;

namespace Football.Modules.Leagues.Application.Players.GetPlayers;

public class GetPlayersQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetPlayersQuery, IReadOnlyCollection<PlayerResponse>>
{
    public async Task<Result<IReadOnlyCollection<PlayerResponse>>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(PlayerResponse.Id)},
                 "Name" AS {nameof(PlayerResponse.Name)},
                 "YellowCard" AS {nameof(PlayerResponse.YellowCard)},
                 "RedCard" AS {nameof(PlayerResponse.RedCard)},
                 "MinutesPlayed" AS {nameof(PlayerResponse.MinutesPlayed)}
             FROM leagues."Player"
             """;

        var players = (await connection.QueryAsync<PlayerResponse>(sql, request)).AsList();

        return Result.Success<IReadOnlyCollection<PlayerResponse>>(players);
    }
}