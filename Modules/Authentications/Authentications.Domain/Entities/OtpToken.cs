using Authentications.Domain.Extensions;

namespace Authentications.Domain.Entities;

/// <summary>
/// Represents a One-Time Password (OTP) token for user authentication.
/// </summary>
public class OtpToken : Entity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpireAt { get; private set; }

    /// <summary>
    /// Determines if the token is expired based on a given date.
    /// </summary>
    public bool IsExpired(DateTime now) => now > ExpireAt;

    /// <summary>
    /// Factory method to create an OTP token for a user.
    /// </summary>
    public static OtpToken Create(Guid userId, DateTime expiration)
    {
        string token = TokenGenerator.GenerateNumericToken();
        return new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpireAt = expiration
        };
    }
}
