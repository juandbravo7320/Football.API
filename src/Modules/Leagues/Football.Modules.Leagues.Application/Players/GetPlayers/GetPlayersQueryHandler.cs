using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetPlayers;

public class GetPlayersQueryHandler(IPlayerReadRepository playerReadRepository) : IQueryHandler<GetPlayersQuery, IReadOnlyCollection<PlayerResponse>>
{
    public async Task<Result<IReadOnlyCollection<PlayerResponse>>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
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

        var players = await playerReadRepository.QueryAsync<PlayerResponse>(sql, request);

        return Result.Success(players);
    }
}