using Bogus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Modules.Leagues.IntergrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new(); 
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}