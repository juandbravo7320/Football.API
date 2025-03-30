using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Matches.GetMatch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Matches;

public class GetMatch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("matches/{id}", async ([FromRoute] int id, ISender sender) =>
        {
            var request = new GetMatchQuery(id);
            var result = await sender.Send(request);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        })
        .RequireAuthorization()
        .WithTags(Tags.Matches);
    }
}