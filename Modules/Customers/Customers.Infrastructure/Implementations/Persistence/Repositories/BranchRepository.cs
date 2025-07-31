namespace Customers.Infrastructure.Implementations.Persistence.Repositories;

public class BranchRepository(IAppDbContext context)
    : Repository<Branch>(context), IBranchRepository
{
    public async Task<IEnumerable<Branch>> GetByEstablishmentIdAsync(Guid establishmentId, 
        CancellationToken cancellationToken = default)
    {
        return await FindAsync(
            predicate: b => b.EstablishmentId == establishmentId,
            cancellationToken: cancellationToken
        );
    }

    public async Task<bool> ExistsAsync(Guid branchId, Guid establishmentId, 
        CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(
            predicate: b => b.Id == branchId && b.EstablishmentId == establishmentId,
            cancellationToken: cancellationToken
        );
    }
}
