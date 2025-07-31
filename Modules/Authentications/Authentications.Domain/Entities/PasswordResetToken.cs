using Authentications.Domain.Extensions;

namespace Authentications.Domain.Entities;

/// <summary>
/// Represents a password reset token assigned to a user.
/// </summary>
public class PasswordResetToken : Entity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpireAt { get; private set; }

    private PasswordResetToken() { }

    private PasswordResetToken(Guid userId, DateTime expiration)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = TokenGenerator.GenerateNumericToken();
        ExpireAt = expiration;
    }

    public static PasswordResetToken Create(Guid userId, DateTime expiration)
        => new(userId, expiration);

    public bool IsExpired(DateTime now) => now > ExpireAt;
}
