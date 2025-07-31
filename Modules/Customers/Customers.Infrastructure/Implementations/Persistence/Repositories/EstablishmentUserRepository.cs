namespace Customers.Infrastructure.Implementations.Persistence.Repositories;

public class EstablishmentUserRepository(IAppDbContext context)
    : Repository<EstablishmentUser>(context), IEstablishmentUserRepository
{
    public async Task<IEnumerable<EstablishmentUser>> GetByEstablishmentIdAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        return await FindAsync(
            predicate: eu => eu.EstablishmentId == establishmentId,
            cancellationToken: cancellationToken
        );
    }

    public async Task<EstablishmentUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await FindFirstAsync(
            predicate: eu => eu.UserId == userId,
            cancellationToken: cancellationToken
        );
    }

    public async Task<bool> ExistsAsync(Guid establishmentId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(
            predicate: eu => eu.EstablishmentId == establishmentId && eu.UserId == userId,
            cancellationToken: cancellationToken
        );
    }
}
