using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Players.GetMinutesPlayed;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Statistics;

public class GetMinutedPlayedCards : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("statistics/player/minutes_played/{playerId:int}", async ([FromRoute] int playerId, ISender sender) =>
        {
            var request = new GetMinutesPlayedQuery(playerId);
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
        })
        .RequireAuthorization()
        .WithTags(Tags.Statistics);
    }
}