using Football.Common.Application.Clock;
using Football.Common.Application.Data;
using Football.Common.Infrastructure.Clock;
using Football.Common.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Football.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}