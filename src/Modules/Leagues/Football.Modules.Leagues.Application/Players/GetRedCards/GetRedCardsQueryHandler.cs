using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetRedCards;

public class GetRedCardsQueryHandler(IPlayerReadRepository playerReadRepository) 
    : IQueryHandler<GetRedCardsQuery, RedCardResponse>
{
    public async Task<Result<RedCardResponse>> Handle(GetRedCardsQuery request, CancellationToken cancellationToken)
    {   
        const string sql = 
            $"""
             SELECT
                "RedCard" AS {nameof(RedCardResponse.Quantity)}
             FROM leagues."Player"
             WHERE "Id" = @PlayerId
             """;

        var redCards =
            await playerReadRepository.QuerySingleOrDefaultAsync<RedCardResponse>(sql, new { PlayerId = request.PlayerId });

        if (redCards is null)
            return Result.Failure<RedCardResponse>(
                PlayerErrors.NotFound(request.PlayerId));

        return Result.Success(redCards);
    }
}