namespace Football.Common.Domain;

public interface IRepository<TEntity, TId>
    where TEntity : class
{
    Task<TEntity?> FindByIdAsync(TId id);
    Task Add (TEntity entity);
    Task AddRange (IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    IQueryable<TEntity> Queryable();
}