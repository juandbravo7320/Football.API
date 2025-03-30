namespace Football.Common.Infrastructure.Authentication;

public class AuthenticationOptions
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ClockSkewSeconds { get; set; }
    public int ExpirationInMinutes { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
}