using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Managers.CreateManager;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Managers;

public class CreateManager : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("managers", async (CreateManagerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
        })
        .WithTags(Tags.Managers);
    }
}