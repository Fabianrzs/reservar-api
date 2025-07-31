namespace Customers.Domain.Repositories;
public interface IBranchRepository : IRepository<Branch>
{
    Task<IEnumerable<Branch>> GetByEstablishmentIdAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid branchId, Guid establishmentId, CancellationToken cancellationToken = default);
}
