using Football.Common.Domain;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Managers.GetManager;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Managers;

public class GetManager : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("managers/{id}", async ([FromRoute] int id, ISender sender) =>
            {
                var request = new GetManagerQuery(id);
                Result<ManagerResponse> result = await sender.Send(request);
                return result.IsFailure 
                    ? Results.BadRequest(result.Error) 
                    : Results.Ok(result.Value);
            })
            .RequireAuthorization()
            .WithTags(Tags.Managers);
    }
}