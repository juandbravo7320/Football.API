using Football.Common.Domain;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Leagues.Presentation.Players;

public class GetPlayers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("players", async (ISender sender) =>
        {
            var request = new GetPlayersQuery();
            Result<IReadOnlyCollection<PlayerResponse>> result = await sender.Send(request);
            return result.IsFailure 
                ? Results.BadRequest(result.Error) 
                : Results.Ok(result.Value);
        })
        .WithTags(Tags.Players);
    }
}