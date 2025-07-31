namespace Common.Infrastructure.Configuration;

public class JwtConfiguration
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string AccessSecret { get; set; } = string.Empty;
    public string RefreshSecret { get; set; } = string.Empty;
    public int AccessExpirationMinutes { get; set; }
    public int RefreshExpirationMinutes { get; set; }
}
