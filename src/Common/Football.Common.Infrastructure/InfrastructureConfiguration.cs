using Football.Common.Application.Clock;
using Football.Common.Application.Data;
using Football.Common.Application.Notification;
using Football.Common.Infrastructure.Authentication;
using Football.Common.Infrastructure.Clock;
using Football.Common.Infrastructure.Data;
using Football.Common.Infrastructure.Notification;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Football.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string databaseConnectionString)
    {
        services.AddHangfire(config =>
        {
            config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(databaseConnectionString);
        });

        services.AddHangfireServer();
        
        services.AddAuthenticationInternal(configuration);
        
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddScoped<INotificationService, SignalRNotificationService>();
        
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}