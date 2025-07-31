namespace Authentications.Infrastructure.Implementations.Persistence.Repositories;

public class PasswordResetTokenRepository(IAppDbContext context)
: Repository<PasswordResetToken>(context), IPasswordResetTokenRepository
{

    public async Task<bool> IsValidTokenAsync(Guid userId, string token, DateTime now, CancellationToken cancellationToken = default)
     => await ExistsAsync(
            predicate: t => t.UserId == userId && t.Token == token && !t.IsExpired(now),
            cancellationToken: cancellationToken
        );
}
