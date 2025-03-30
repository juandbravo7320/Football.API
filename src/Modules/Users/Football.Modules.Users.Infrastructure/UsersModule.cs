using Football.Common.Presentation.Endpoints;
using Football.Modules.Users.Application.Abstractions.Data;
using Football.Modules.Users.Application.Authentication;
using Football.Modules.Users.Domain.Users;
using Football.Modules.Users.Infrastructure.Authentication;
using Football.Modules.Users.Infrastructure.Database;
using Football.Modules.Users.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        
        services.AddInfrastructure(configuration);
        
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        var databaseConnectionString = configuration.GetConnectionString("Database")!;
        
        services.AddDbContext<UsersDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users)));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());
            
        services.AddScoped<IUserRepository, UserRepository>();
    }
}