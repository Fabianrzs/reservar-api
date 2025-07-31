using Common.Application.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.Infrastructure.Contexts;

/// <summary>
/// Abstract base class for application DbContexts, providing shared functionality
/// and enforcing implementation of IAppDbContext.
/// </summary>
public abstract class AppDbContextBase : DbContext, IAppDbContext
{
    protected AppDbContextBase() { }
    protected AppDbContextBase(string ConnectionsString, string Shemma) { }
    protected AppDbContextBase(DbContextOptions options) : base(options) { }

    /// <summary>
    /// Gets a DbSet for the specified entity type.
    /// </summary>
    public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();

    /// <summary>
    /// Gets the EntityEntry for tracking an entity.
    /// </summary>
    public new EntityEntry Entry(object entity) => base.Entry(entity);

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Add logic here for auditing, soft delete, etc., if needed.
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Attaches an entity to the context for tracking.
    /// </summary>
    public new void Attach<TEntity>(TEntity entity) where TEntity : class => base.Attach(entity);


    /// <summary>
    /// Attaches a range of entities to the context for tracking.
    /// </summary>
    public void AttachRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class => base.AttachRange(entities);
}
