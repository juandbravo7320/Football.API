using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Referees.UpdateReferee;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Referees;

public class UpdateReferee : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("referees", async (UpdateRefereeCommand request, ISender sender) =>
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