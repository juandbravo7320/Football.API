using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Players.CreatePlayer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Players;

public class CreatePlayer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("players", async (CreatePlayerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
        })
        .WithTags(Tags.Players);
    }
}