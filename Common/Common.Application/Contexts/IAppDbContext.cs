using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.Application.Contexts;

/// <summary>
/// Application-wide abstraction for EF Core DbContext interaction.
/// Enables decoupling and supports dependency injection.
/// </summary>
public interface IAppDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
    void AttachRange(params object[] entities);
}
