using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetPlayer;

public class GetPlayerQueryHandler(IPlayerReadRepository playerReadRepository) : IQueryHandler<GetPlayerQuery, PlayerResponse>
{
    public async Task<Result<PlayerResponse>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {   
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

        var player = await playerReadRepository.QuerySingleOrDefaultAsync<PlayerResponse>(sql, new { PlayerId = request.Id });
        
        if (player is null)
            return Result.Failure<PlayerResponse>(PlayerErrors.NotFound(request.Id));

        return Result.Success(player);
    }
}