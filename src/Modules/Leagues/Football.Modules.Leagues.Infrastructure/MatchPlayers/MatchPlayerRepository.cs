using Football.Modules.Leagues.Domain.MatchPlayers;
using Football.Modules.Leagues.Infrastructure.Abstractions;
using Football.Modules.Leagues.Infrastructure.Database;

namespace Football.Modules.Leagues.Infrastructure.MatchPlayers;

public class MatchPlayerRepository : Repository<MatchPlayer, int>, IMatchPlayerRepository
{
    public MatchPlayerRepository(LeaguesDbContext dbContext) : base(dbContext)
    {
    }
}