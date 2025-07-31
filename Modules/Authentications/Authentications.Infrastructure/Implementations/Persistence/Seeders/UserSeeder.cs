using Authentications.Domain.Enums;
using Authentications.Domain.ValueObjects;

namespace Authentications.Infrastructure.Implementations.Persistence.Seeders;

public class UsersSeeder : ISeeder<AuthenticationDbContext>
{
    public async Task SeedAsync(AuthenticationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Users.AnyAsync(u => u.AuthProvider == AuthProvider.Local, cancellationToken))
        {
            DateTime now = DateTime.UtcNow;

            var users = new List<User>
            {
                User.Create("admin@reservar.com", AuthProvider.Local, "Admin Reservar"),
            };

            var credentials = new List<UserCredentials>
            {
                UserCredentials.Create(users[0].Id, HashedPassword.Create("Admin123*"), now),
            };

            await context.Users.AddRangeAsync(users, cancellationToken);
            await context.UserCredentials.AddRangeAsync(credentials, cancellationToken);
        }
    }
}
