namespace Authentications.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; }

    public ICollection<UserRole> Users { get; private set; } = [];
    public ICollection<RolePermission> Permissions { get; private set; } = [];
    private Role() { } 

    private Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    public static Role Create(string name) => new(name);
}
