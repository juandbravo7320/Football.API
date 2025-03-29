using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Infrastructure.Abstractions;
using Football.Modules.Leagues.Infrastructure.Database;

namespace Football.Modules.Leagues.Infrastructure.Managers;

public class ManagerRepository : Repository<Manager, int>, IManagerRepository
{
    public ManagerRepository(LeaguesDbContext dbContext) : base(dbContext)
    {
    }
}