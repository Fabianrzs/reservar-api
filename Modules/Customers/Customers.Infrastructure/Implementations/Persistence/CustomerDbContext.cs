using Common.Infrastructure.Contexts;

namespace Customers.Infrastructure.Implementations.Persistence;

public class CustomersDbContext(DbContextOptions<CustomersDbContext> options)
    : AppDbContextBase(options)
{
    public DbSet<Establishment> Establishments => Set<Establishment>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<EstablishmentUser> EstablishmentUsers => Set<EstablishmentUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customers");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
