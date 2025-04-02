namespace Football.Common.Domain;

public interface IReadRepository<TEntity>
    where TEntity : class
{
    Task<IReadOnlyCollection<TEntity>> QueryAsync(string sql, object request);
    Task<TEntity?> QuerySingleOrDefaultAsync(string sql, object request);
}