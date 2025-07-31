using Authentications.Domain.Entities;

namespace Authentications.Domain.Repositories;

public interface IUserCredentialsRepository : IRepository<UserCredentials>
{
    Task<UserCredentials?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
