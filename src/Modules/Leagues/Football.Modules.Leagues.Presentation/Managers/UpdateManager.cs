using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Managers.UpdateManager;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Managers;

public class UpdateManager : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("managers", async (UpdateManagerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
        })
        .WithTags(Tags.Managers);
    }
}