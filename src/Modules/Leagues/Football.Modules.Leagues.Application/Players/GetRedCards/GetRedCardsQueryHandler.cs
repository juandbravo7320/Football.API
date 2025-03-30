using Dapper;
using Football.Common.Application.Data;
using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetRedCards;

public class GetRedCardsQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetRedCardsQuery, RedCardResponse>
{
    public async Task<Result<RedCardResponse>> Handle(GetRedCardsQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
             SELECT
                "RedCard" AS {nameof(RedCardResponse.Quantity)}
             FROM leagues."Player"
             WHERE "Id" = @PlayerId
             """;

        var redCards = await connection.QuerySingleOrDefaultAsync<RedCardResponse>(sql, new { PlayerId = request.PlayerId });

        if (redCards is null)
            return Result.Failure<RedCardResponse>(
                PlayerErrors.NotFound(request.PlayerId));

        return Result.Success(redCards);
    }
}