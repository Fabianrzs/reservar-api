namespace Authentications.Infrastructure.Implementations.Persistence.Seeders;

public class RolesSeeder : ISeeder<AuthenticationDbContext>
{
    public async Task SeedAsync(AuthenticationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Roles.AnyAsync(cancellationToken))
        {
            var roles = new List<Role>
            {
                Role.Create("Administrator"),
                Role.Create("Tenant"),
                Role.Create("User")
            };

            await context.Roles.AddRangeAsync(roles, cancellationToken);
        }
    }
}
