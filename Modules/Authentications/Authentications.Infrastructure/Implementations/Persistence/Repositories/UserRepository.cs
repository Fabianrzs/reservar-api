using Authentications.Domain.Enums;

namespace Authentications.Infrastructure.Implementations.Persistence.Repositories;

public class UserRepository(IAppDbContext context)
    : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, 
        CancellationToken cancellationToken = default)
        => await FindFirstAsync(u => u.Email == email,
            cancellationToken: cancellationToken);
    public async Task<User?> GetByEmailAsync(string email, 
        AuthProvider authProvider,
        CancellationToken cancellationToken = default)
        => await FindFirstAsync(u => u.Email == email 
            && u.AuthProvider == authProvider, 
            includes: [u => u.Roles],
            cancellationToken: cancellationToken);

    public async Task<bool> ExistsByEmailAsync(string email, 
        CancellationToken cancellationToken = default)
        => await ExistsAsync(u => u.Email == email, 
            cancellationToken: cancellationToken);

}
