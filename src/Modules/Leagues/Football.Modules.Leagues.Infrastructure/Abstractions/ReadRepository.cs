using System.Data.Common;
using Dapper;
using Football.Common.Application.Data;
using Football.Common.Domain;

namespace Football.Modules.Leagues.Infrastructure.Abstractions;

public class ReadRepository(IDbConnectionFactory dbConnectionFactory) : IReadRepository
{
    public async Task<IReadOnlyCollection<TResult>> QueryAsync<TResult>(string sql, object request)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        return (await connection.QueryAsync<TResult>(sql, request)).AsList();
    }

    public async Task<TResult?> QuerySingleOrDefaultAsync<TResult>(string sql, object request)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<TResult>(sql, request);
    }
}