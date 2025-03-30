using Football.Modules.Leagues.Infrastructure.Database;
using Football.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FootballAPI.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        ApplyMigrations<LeaguesDbContext>(scope);
        ApplyMigrations<UsersDbContext>(scope);
    }

    private static void ApplyMigrations<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        context.Database.Migrate();
    }
}