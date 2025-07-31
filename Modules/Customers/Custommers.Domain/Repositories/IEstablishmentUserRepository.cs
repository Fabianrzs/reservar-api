namespace Customers.Domain.Repositories;

public interface IEstablishmentUserRepository : IRepository<EstablishmentUser>
{
    Task<IEnumerable<EstablishmentUser>> GetByEstablishmentIdAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<EstablishmentUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid establishmentId, Guid userId, CancellationToken cancellationToken = default);
}
