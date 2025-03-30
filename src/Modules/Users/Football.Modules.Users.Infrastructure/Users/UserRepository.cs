using Football.Modules.Users.Domain.Users;
using Football.Modules.Users.Infrastructure.Abstractions;
using Football.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Football.Modules.Users.Infrastructure.Users;

public class UserRepository : Repository<User, int>, IUserRepository
{
    public UserRepository(UsersDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await dbSet
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }
}