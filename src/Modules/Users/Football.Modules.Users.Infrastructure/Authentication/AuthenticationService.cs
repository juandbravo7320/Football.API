using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Football.Common.Application.Clock;
using Football.Modules.Users.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using AuthenticationOptions = Football.Common.Infrastructure.Authentication.AuthenticationOptions;
using IAuthenticationService = Football.Modules.Users.Application.Authentication.IAuthenticationService;

namespace Football.Modules.Users.Infrastructure.Authentication;

public class AuthenticationService(
    IDateTimeProvider dateTimeProvider,
    AuthenticationOptions authenticationOptions) : IAuthenticationService
{
    public string GenerateAccessToken(User user)
    {
        IEnumerable<Claim> claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, string.Concat(user.FirstName, user.LastName)),
            new (ClaimTypes.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            claims: claims,
            expires: dateTimeProvider.UtcNow.AddMinutes(authenticationOptions.ExpirationInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}