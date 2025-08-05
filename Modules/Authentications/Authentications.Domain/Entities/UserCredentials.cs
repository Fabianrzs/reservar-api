using Authentications.Domain.ValueObjects;
using PasswordGenerator;
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

    public static (UserCredentials Credentials, string PlainPassword) CreateWithGeneratedPassword(Guid userId, DateTime passwordSetAt)
    {
        var pwdGen = new Password(includeLowercase: true, includeUppercase: true, 
            includeNumeric: true, includeSpecial: true, passwordLength: 12);

        string plainPassword = pwdGen.Next();

        var hashedPassword = HashedPassword.HashPassword(plainPassword);

        var credentials = new UserCredentials
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CurrentPassword = hashedPassword,
            PasswordSetAt = passwordSetAt,
            PasswordHistory =
            [
                HashedPasswordHistory.From(hashedPassword, passwordSetAt)
            ]
        };

        return (credentials, plainPassword);
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
