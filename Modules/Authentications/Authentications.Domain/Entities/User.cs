using Authentications.Domain.Enums;

namespace Authentications.Domain.Entities;

public class User : Entity
{
    public string Email { get; private set; }
    public string? Name { get; private set; }
    public AuthProvider AuthProvider { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<UserRole> Roles { get; private set; } = [];

    public static User Create(string email, AuthProvider provider, string? name = null)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            AuthProvider = provider,
            Name = name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public bool IsExternalUser() => AuthProvider != AuthProvider.Local;
}
