using Football.Common.Domain;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Referees.GetReferees;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Referees;

public class GetReferees : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("referees", async (ISender sender) =>
        {
            var request = new GetRefereesQuery();
            Result<IReadOnlyCollection<RefereeResponse>> result = await sender.Send(request);
            return result.IsFailure 
                ? Results.BadRequest(result.Error) 
                : Results.Ok(result.Value);
        })
        .WithTags(Tags.Referees);
    }
}