using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Referees.CreateReferee;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Referees;

public class CreateReferee : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("referees", async (CreateRefereeCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
        })
        .RequireAuthorization()
        .WithTags(Tags.Referees);
    }
}