using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Domain.MatchPlayers;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.Domain.Referees;
using Football.Modules.Leagues.Infrastructure.Database;
using Football.Modules.Leagues.Infrastructure.Managers;
using Football.Modules.Leagues.Infrastructure.Matches;
using Football.Modules.Leagues.Infrastructure.MatchPlayers;
using Football.Modules.Leagues.Infrastructure.Players;
using Football.Modules.Leagues.Infrastructure.Referees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Modules.Leagues.Infrastructure;

public static class LeaguesModule
{
    public static IServiceCollection AddLeaguesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        
        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<LeaguesDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Leagues)));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LeaguesDbContext>());

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IRefereeRepository, RefereeRepository>();
        services.AddScoped<IMatchPlayerRepository, MatchPlayerRepository>();
    }
}