using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Infrastructure.Abstractions;
using Football.Modules.Leagues.Infrastructure.Database;

namespace Football.Modules.Leagues.Infrastructure.Matches;

public class MatchRepository : Repository<Match, int>, IMatchRepository
{
    public MatchRepository(LeaguesDbContext dbContext) : base(dbContext)
    {
    }
}