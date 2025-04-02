namespace Football.Common.Domain;

public interface IReadRepository
{
    Task<IReadOnlyCollection<TResult>> QueryAsync<TResult>(string sql, object request);
    Task<TResult?> QuerySingleOrDefaultAsync<TResult>(string sql, object request);
}