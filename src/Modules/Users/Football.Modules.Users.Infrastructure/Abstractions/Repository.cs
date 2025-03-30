using Football.Common.Domain;
using Football.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Football.Modules.Users.Infrastructure.Abstractions;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class
{
    private readonly UsersDbContext _dbContext;
    public DbSet<TEntity> dbSet { get; }

    public Repository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> FindByIdAsync(TId id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task Add(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await dbSet.AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
        dbSet.Remove(entity);
    }
    
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        dbSet.RemoveRange(entities);
    }
    
    public IQueryable<TEntity> Queryable()
    {
        return dbSet;
    }
}