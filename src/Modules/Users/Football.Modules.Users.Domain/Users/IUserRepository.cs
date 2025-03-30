using Football.Common.Domain;

namespace Football.Modules.Users.Domain.Users;

public interface IUserRepository : IRepository<User, int>
{
    Task<User?> FindByEmailAsync(string email);
}