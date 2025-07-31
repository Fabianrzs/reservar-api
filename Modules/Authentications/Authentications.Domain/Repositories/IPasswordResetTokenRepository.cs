using Authentications.Domain.Entities;

namespace Authentications.Domain.Repositories;

public interface IPasswordResetTokenRepository : IRepository<PasswordResetToken>
{
    Task<bool> IsValidTokenAsync(Guid userId, string token, DateTime now, CancellationToken cancellationToken = default);
}
