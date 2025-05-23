using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.GetMinutesPlayed;

public class GetMinutesPlayedQueryHandler(IPlayerReadRepository playerReadRepository) 
    : IQueryHandler<GetMinutesPlayedQuery, MinutesPlayedResponse>
{
    public async Task<Result<MinutesPlayedResponse>> Handle(GetMinutesPlayedQuery request, CancellationToken cancellationToken)
    {   
        const string sql = 
            $"""
             SELECT
                "MinutesPlayed" AS {nameof(MinutesPlayedResponse.Quantity)}
             FROM leagues."Player"
             WHERE "Id" = @PlayerId
             """;

        var minutesPlayed =
            await playerReadRepository.QuerySingleOrDefaultAsync<MinutesPlayedResponse>(sql,new { PlayerId = request.PlayerId });

        if (minutesPlayed is null)
            return Result.Failure<MinutesPlayedResponse>(
                PlayerErrors.NotFound(request.PlayerId));

        return Result.Success(minutesPlayed);
    }
}