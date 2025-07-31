namespace Authentications.Domain.Entities;

public class RolePermission : Entity
{
    public Guid RoleId { get; }
    public Guid PermissionId { get; }

    public Role Role { get; private set; } = default!;
    public Permission Permission { get; private set; } = default!;

    private RolePermission() { }

    private RolePermission(Role role, Permission permission)
    {
        Role = role;
        Permission = permission;
    }
    public static RolePermission Create(Role role, Permission permission) => new(role, permission);
}
