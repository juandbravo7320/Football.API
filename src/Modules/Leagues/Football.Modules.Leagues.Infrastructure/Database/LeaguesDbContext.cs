using System.Data.Common;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Domain.MatchPlayers;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.Domain.Referees;
using Football.Modules.Leagues.Infrastructure.Managers;
using Football.Modules.Leagues.Infrastructure.Matches;
using Football.Modules.Leagues.Infrastructure.MatchPlayers;
using Football.Modules.Leagues.Infrastructure.Players;
using Football.Modules.Leagues.Infrastructure.Referees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Football.Modules.Leagues.Infrastructure.Database;

public sealed class LeaguesDbContext(DbContextOptions<LeaguesDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Player> Players { get; set; }
    internal DbSet<Manager> Managers { get; set; }
    internal DbSet<Referee> Referees { get; set; }
    internal DbSet<Match> Matches { get; set; }
    internal DbSet<MatchPlayer> MatchesPlayers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Leagues);

        modelBuilder.ApplyConfiguration(new PlayerConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new RefereeConfiguration());
        modelBuilder.ApplyConfiguration(new MatchConfiguration());
        modelBuilder.ApplyConfiguration(new MatchPlayersConfiguration());
    }
    
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }
        
        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}