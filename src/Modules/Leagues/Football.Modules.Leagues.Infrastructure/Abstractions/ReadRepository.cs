using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Domain;

namespace Football.Modules.Leagues.Infrastructure.Abstractions;

public class ReadRepository<TEntity>(IDbConnectionFactory dbConnectionFactory) 
    : IReadRepository<TEntity> where TEntity : class
{
    public async Task<IReadOnlyCollection<TEntity>> QueryAsync(string sql, object request)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        return (await connection.QueryAsync<TEntity>(sql, request)).AsList();
    }

    public async Task<TEntity?> QuerySingleOrDefaultAsync(string sql, object request)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<TEntity>(sql, request);
    }
}