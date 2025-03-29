using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Matches.UpdateMatch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Matches;

public class UpdateMatch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("matches", async ([FromBody] UpdateMatchCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);
            
            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Matches);
    }
}