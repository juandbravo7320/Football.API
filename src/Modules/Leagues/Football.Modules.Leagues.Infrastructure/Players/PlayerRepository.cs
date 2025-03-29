using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.Infrastructure.Abstractions;
using Football.Modules.Leagues.Infrastructure.Database;

namespace Football.Modules.Leagues.Infrastructure.Players;

public class PlayerRepository : Repository<Player, int>, IPlayerRepository
{
    public PlayerRepository(LeaguesDbContext dbContext) : base(dbContext)
    {
    }
}