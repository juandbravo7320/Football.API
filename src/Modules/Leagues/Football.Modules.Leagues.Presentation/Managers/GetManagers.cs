using Football.Common.Domain;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Managers;

public class GetManagers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("managers", async (ISender sender) =>
        {
            var request = new GetManagersQuery();
            Result<IReadOnlyCollection<ManagerResponse>> result = await sender.Send(request);
            return result.IsFailure 
                ? Results.BadRequest(result.Error) 
                : Results.Ok(result.Value);
        })
        .RequireAuthorization()
        .WithTags(Tags.Managers);
    }
}