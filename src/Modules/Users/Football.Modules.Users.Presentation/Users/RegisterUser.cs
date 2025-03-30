using Football.Common.Presentation.Endpoints;
using Football.Modules.Users.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Football.Modules.Users.Presentation.Users;

public class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async ([FromBody] RegisterUserCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Users);
    }
}