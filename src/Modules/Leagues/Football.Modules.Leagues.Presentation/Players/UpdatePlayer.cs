using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Players.UpdatePlayer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Players;

public class UpdatePlayer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("players", async (UpdatePlayerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
        })
        .WithTags(Tags.Players);
    }
}