namespace Customers.Infrastructure.Implementations.Persistence.Repositories;

public class EstablishmentRepository(IAppDbContext context)
    : Repository<Establishment>(context), IEstablishmentRepository
{
    public async Task<Establishment?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Establishment>()
            .Include(e => e.ContactInfo) 
            .Include(e => e.Users)
            .Include(e => e.Branches)
            .FirstOrDefaultAsync(e => e.Users.Any(u => u.UserId == userId), cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(
            predicate: e => e.Name == name,
            cancellationToken: cancellationToken
        );
    }
}
