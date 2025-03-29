using System.Data.Common;

namespace Football.Modules.Leagues.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}