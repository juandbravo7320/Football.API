using Football.Modules.Users.Domain.Users;

namespace Football.Modules.Users.Application.Authentication;

public interface IAuthenticationService
{
    string GenerateAccessToken(User user);
}