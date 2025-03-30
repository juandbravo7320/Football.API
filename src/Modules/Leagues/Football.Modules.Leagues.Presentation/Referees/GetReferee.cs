using Football.Common.Domain;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Referees.GetReferee;
using Football.Modules.Leagues.Application.Referees.GetReferees;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Referees;

public class GetReferee : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("referees/{id}", async ([FromRoute] int id, ISender sender) =>
            {
                var request = new GetRefereeQuery(id);
                Result<RefereeResponse> result = await sender.Send(request);
                return result.IsFailure 
                    ? Results.BadRequest(result.Error) 
                    : Results.Ok(result.Value);
            })
            .RequireAuthorization()
            .WithTags(Tags.Referees);
    }
}