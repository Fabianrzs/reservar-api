using Authentications.Domain.Entities;

namespace Authentications.Domain.Repositories;

public interface IOtpTokenRepository : IRepository<OtpToken>
{
    Task<bool> IsValidTokenForUserAsync(Guid userId, string token, DateTime dateNow, CancellationToken cancellationToken = default);
}
