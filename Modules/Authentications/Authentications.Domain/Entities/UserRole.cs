namespace Authentications.Domain.Entities;

public class UserRole : Entity
{
    public Guid UserId { get; }
    public Guid RoleId { get; }

    public User User { get; private set; } = default!;
    public Role Role { get; private set; } = default!;
}
