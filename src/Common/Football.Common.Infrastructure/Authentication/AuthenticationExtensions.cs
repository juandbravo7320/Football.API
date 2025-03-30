using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Football.Common.Infrastructure.Authentication;

public static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authenticationSettings = configuration.GetSection("Authentication").Get<AuthenticationOptions>() ??
                                     throw new ApplicationException("authentication settings are unavailable");
        
        // Registrar AuthenticationOptions con valores modificados
        services.Configure<AuthenticationOptions>(options =>
        {
            options.Secret = authenticationSettings.Secret;
            options.Issuer = authenticationSettings.Issuer;
            options.Audience = authenticationSettings.Audience;
            options.ClockSkewSeconds = authenticationSettings.ClockSkewSeconds;
            options.ExpirationInMinutes = authenticationSettings.ExpirationInMinutes;
            options.ValidateIssuer = authenticationSettings.ValidateIssuer;
            options.ValidateAudience = authenticationSettings.ValidateAudience;
        });
        
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<AuthenticationOptions>>().Value);
         
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = authenticationSettings.ValidateIssuer,
                ValidateAudience = authenticationSettings.ValidateAudience,
                ValidIssuer = authenticationSettings.Issuer,
                ValidAudience = authenticationSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(authenticationSettings.ClockSkewSeconds),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Secret)),
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true
            };
        });
        
        return services;
    }
}