namespace Authentications.Infrastructure.Implementations.Persistence.Seeders;

public class PermissionsSeeder : ISeeder<AuthenticationDbContext>
{
    public async Task SeedAsync(AuthenticationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Permissions.AnyAsync(cancellationToken))
        {
            var permissions = new List<Permission>
            {
                Permission.Create("DASHBOARD_VIEW", "Access dashboard"),
                Permission.Create("RESERVATION_MANAGE", "Manage reservations"),
                Permission.Create("REPORTS_VIEW", "View reports"),
                Permission.Create("USERS_MANAGE", "Manage users")
            };

            await context.Permissions.AddRangeAsync(permissions, cancellationToken);
        }
    }
}
