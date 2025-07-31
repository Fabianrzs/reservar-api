namespace Authentications.Infrastructure.Implementations.Persistence.Repositories;

public class UserCredentialsRepository(IAppDbContext context)
    : Repository<UserCredentials>(context), IUserCredentialsRepository
{
    public async Task<UserCredentials?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await FindFirstAsync(
            predicate: x => x.UserId == userId,
            includes: [x => x.PasswordHistory],
            cancellationToken: cancellationToken
        );
}

