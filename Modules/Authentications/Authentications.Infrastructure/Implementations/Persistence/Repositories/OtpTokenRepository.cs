namespace Authentications.Infrastructure.Implementations.Persistence.Repositories;

public class OtpTokenRepository(IAppDbContext context)
: Repository<OtpToken>(context), IOtpTokenRepository
{
    public async Task<bool> IsValidTokenForUserAsync(Guid userId, string token, DateTime dateNow, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(
            predicate: x => x.UserId == userId
                && x.ExpireAt > dateNow
                && x.Token == token,
            cancellationToken: cancellationToken
        );
    }
}
