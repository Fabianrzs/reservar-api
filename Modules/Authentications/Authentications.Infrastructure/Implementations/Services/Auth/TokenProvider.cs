using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentications.Application.Abstractions.Services.Auth;
using Common.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentications.Infrastructure.Implementations.Services.Auth;

public sealed class TokenProvider(IOptions<JwtConfiguration> options) : ITokenProvider
{
    private readonly JwtConfiguration jwtOptions = options?.Value 
        ?? throw new ArgumentNullException(nameof(options));

    public string GenerateAccessToken(Guid sessionId, User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>
        {
            new("SessionId", sessionId.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            .ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
        };

        foreach (UserRole userRole in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        }

        return GenerateToken(claims, jwtOptions.AccessExpirationMinutes, jwtOptions.AccessSecret);
    }

    public string GenerateRefreshToken(Guid sessionId, Guid userId)
    {
        var claims = new List<Claim>
        {
            new("SessionId", sessionId.ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            .ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
        };

        return GenerateToken(claims, jwtOptions.RefreshExpirationMinutes, jwtOptions.RefreshSecret);
    }

    private string GenerateToken(IEnumerable<Claim> claims, int expirationMinutes, string secret)
    {
        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new InvalidOperationException("JWT secret cannot be null or empty.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = credentials,
            Issuer = jwtOptions.Issuer,
            Audience = jwtOptions.Audience
        };

        var handler = new JwtSecurityTokenHandler();
        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
