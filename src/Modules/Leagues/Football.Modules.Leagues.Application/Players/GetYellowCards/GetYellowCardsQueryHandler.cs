using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetYellowCards;

public class GetYellowCardsQueryHandler(IPlayerReadRepository playerReadRepository) 
    : IQueryHandler<GetYellowCardsQuery, YellowCardResponse>
{
    public async Task<Result<YellowCardResponse>> Handle(GetYellowCardsQuery request, CancellationToken cancellationToken)
    {   
        const string sql = 
            $"""
             SELECT
                "YellowCard" AS {nameof(YellowCardResponse.Quantity)}
             FROM leagues."Player"
             WHERE "Id" = @PlayerId
             """;

        var yellowCards =
            await playerReadRepository.QuerySingleOrDefaultAsync<YellowCardResponse>(sql, new { PlayerId = request.PlayerId });

        if (yellowCards is null)
            return Result.Failure<YellowCardResponse>(
                PlayerErrors.NotFound(request.PlayerId));

        return Result.Success(yellowCards);
    }
}