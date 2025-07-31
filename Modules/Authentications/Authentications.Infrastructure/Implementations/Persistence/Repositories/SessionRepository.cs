using Authentications.Domain.Enums;

namespace Authentications.Infrastructure.Implementations.Persistence.Repositories;

public class SessionRepository(IAppDbContext context)
: Repository<Session>(context), ISessionRepository
{
    public async Task InactivateAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        Session? session = await GetByIdAsync(sessionId,cancellationToken: cancellationToken);
        if (session is null) { return; }
        session.Expire();
        await UpdateAsync(session, cancellationToken);
    }

    public async Task<Session> GetActiveAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await FindFirstAsync(
            predicate: s => s.UserId == userId && s.Status == SessionStatus.Active,
            cancellationToken: cancellationToken
        );
    }

    public async Task<List<Session>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        IEnumerable<Session> sessions = await FindAsync(
            predicate: s => s.UserId == userId && s.Status == SessionStatus.Active,
            cancellationToken: cancellationToken
        );
        return [.. sessions];
    }
}
