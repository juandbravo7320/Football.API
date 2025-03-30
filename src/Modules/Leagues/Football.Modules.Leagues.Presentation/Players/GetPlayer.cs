using Football.Common.Domain;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Players.GetPlayer;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Players;

public class GetPlayer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("players/{id}", async ([FromRoute] int id, ISender sender) =>
            {
                var request = new GetPlayerQuery(id);
                Result<PlayerResponse> result = await sender.Send(request);
                return result.IsFailure 
                    ? Results.BadRequest(result.Error) 
                    : Results.Ok(result.Value);
            })
            .RequireAuthorization()
            .WithTags(Tags.Players);
    }
}