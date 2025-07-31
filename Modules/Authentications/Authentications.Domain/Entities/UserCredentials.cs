using Authentications.Domain.ValueObjects;

namespace Authentications.Domain.Entities;

public class UserCredentials : Entity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;

    public HashedPassword CurrentPassword { get; private set; }
    public DateTime PasswordSetAt { get; private set; }

    public List<HashedPasswordHistory> PasswordHistory { get; private set; } = [];

    public static UserCredentials Create(Guid userId, HashedPassword password, DateTime passwordSetAt)
    {
        return new UserCredentials
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CurrentPassword = password,
            PasswordSetAt = passwordSetAt,
            PasswordHistory =
            [
                HashedPasswordHistory.From(password, passwordSetAt)
            ]
        };
    }

    public void RotatePassword(HashedPassword newPassword, DateTime passwordSetAt)
    {
        if (CurrentPassword == newPassword)
        {
            throw new InvalidOperationException("The new password cannot be the same as the current one.");
        }

        PasswordHistory.Add(HashedPasswordHistory.From(CurrentPassword, PasswordSetAt));
        CurrentPassword = newPassword;
        PasswordSetAt = passwordSetAt;
    }

    public bool IsPasswordExpired(TimeSpan expirationPeriod)
        => DateTime.UtcNow - PasswordSetAt > expirationPeriod;

    public bool WasUsedBefore(HashedPassword password)
        => PasswordHistory.Any(p => p.Password == password);
}
