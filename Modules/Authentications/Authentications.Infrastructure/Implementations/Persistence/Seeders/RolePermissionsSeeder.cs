namespace Authentications.Infrastructure.Implementations.Persistence.Seeders;

public class RolePermissionsSeeder : ISeeder<AuthenticationDbContext>
{
    public async Task SeedAsync(AuthenticationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.RolePermissions.AnyAsync(cancellationToken))
        {
            Role? adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Administrator", cancellationToken);
            Role? tenantRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Tenant", cancellationToken);
            Role? userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "User", cancellationToken);

            Permission dashboard = await context.Permissions.FirstAsync(p => p.Code == "DASHBOARD_VIEW", cancellationToken);
            Permission reservation = await context.Permissions.FirstAsync(p => p.Code == "RESERVATION_MANAGE", cancellationToken);
            Permission reports = await context.Permissions.FirstAsync(p => p.Code == "REPORTS_VIEW", cancellationToken);
            Permission users = await context.Permissions.FirstAsync(p => p.Code == "USERS_MANAGE", cancellationToken);

            var rolePermissions = new List<RolePermission>();

            if (adminRole is not null)
            {
                rolePermissions.AddRange([
                    RolePermission.Create(adminRole, dashboard),
                    RolePermission.Create(adminRole, reservation),
                    RolePermission.Create(adminRole, reports),
                    RolePermission.Create(adminRole, users)
                ]);
            }

            if (userRole is not null)
            {
                rolePermissions.AddRange([
                    RolePermission.Create(userRole, dashboard),
                    RolePermission.Create(userRole, reservation)
                ]);
            }

            if (tenantRole is not null)
            {
                rolePermissions.AddRange([
                    RolePermission.Create(tenantRole, dashboard),
                    RolePermission.Create(tenantRole, reservation),
                    RolePermission.Create(tenantRole, reports)
                ]);
            }
            
            await context.RolePermissions.AddRangeAsync(rolePermissions, cancellationToken);
        }
    }
}
