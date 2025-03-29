using Football.Modules.Leagues.Domain.Referees;
using Football.Modules.Leagues.Infrastructure.Abstractions;
using Football.Modules.Leagues.Infrastructure.Database;

namespace Football.Modules.Leagues.Infrastructure.Referees;

public class RefereeRepository : Repository<Referee, int>, IRefereeRepository
{
    public RefereeRepository(LeaguesDbContext dbContext) : base(dbContext)
    {
    }
}