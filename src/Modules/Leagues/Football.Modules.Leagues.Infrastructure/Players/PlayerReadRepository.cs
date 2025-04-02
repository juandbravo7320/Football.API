using Football.Common.Application.Data;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.Infrastructure.Abstractions;

namespace Football.Modules.Leagues.Infrastructure.Players;

public class PlayerReadRepository(IDbConnectionFactory dbConnectionFactory) : ReadRepository(dbConnectionFactory), IPlayerReadRepository
{
    
}