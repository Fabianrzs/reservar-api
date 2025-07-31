namespace Customers.Domain.Repositories;

public interface IEstablishmentRepository : IRepository<Establishment>
{
    Task<Establishment?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}
