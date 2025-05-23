using Microsoft.AspNetCore.Routing;

namespace Football.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}