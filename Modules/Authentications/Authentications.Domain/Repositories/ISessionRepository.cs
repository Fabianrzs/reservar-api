using Authentications.Domain.Entities;

namespace Authentications.Domain.Repositories;

public interface ISessionRepository : IRepository<Session>
{
    Task<List<Session>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Session> GetActiveAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken = default);
    Task InactivateAsync(Guid sessionId, CancellationToken cancellationToken = default);
}
