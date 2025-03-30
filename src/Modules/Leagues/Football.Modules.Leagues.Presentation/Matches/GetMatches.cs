using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Matches.GetMatches;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Matches;

public class GetMatches : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("matches", async (ISender sender) =>
        {
            var request = new GetMatchesQuery();
            var result = await sender.Send(request);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        })
        .RequireAuthorization()
        .WithTags(Tags.Matches);
    }
}