using Football.Common.Application.Data;
using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Infrastructure.Abstractions;

namespace Football.Modules.Leagues.Infrastructure.Managers;

public class ManagerReadRepository(IDbConnectionFactory dbConnectionFactory) 
    : ReadRepository<Manager>(dbConnectionFactory), IManagerReadRepository
{
    
}