using System.Linq.Expressions;
using Common.Application.Contexts;
using Common.Domain;
using Common.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for basic CRUD and query operations.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{

    protected readonly IAppDbContext Context;
    public Repository(IAppDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        if (context.Set<TEntity>() == null)
        {
            throw new InvalidOperationException($"DbSet for entity {typeof(TEntity).Name} is not registered in the context.");
        }
    }


    #region Read Methods

    public virtual async Task<PagedResult<TEntity>> PaginateAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber <= 0)  {  pageNumber = 1; }

        if (pageSize <= 0) { pageSize = 10; }

        IQueryable<TEntity> query = Context.Set<TEntity>().AsNoTracking();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        query = query.ApplyIncludes(includes);

        int totalCount = await query.CountAsync(cancellationToken);

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        List<TEntity> items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(items, totalCount, pageNumber, pageSize);
    }


public virtual async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ApplyIncludes(includes);

        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ApplyIncludes(includes);

        return await query.CountAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        Guid id,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(e => e.Id == id)
            .ApplyIncludes(includes);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>()
            .AsNoTracking()
            .ApplyIncludes(includes);

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ApplyIncludes(includes);

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> FindFirstAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ApplyIncludes(includes);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    #endregion

    #region Create Methods

    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Update Methods

    public virtual async Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().Update(entity);
        await Task.CompletedTask;
    }

    public virtual async Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Delete Methods

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        TEntity entity = await Context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException($"Entity with ID {id} was not found.");
        entity.Inactivate();
        Context.Attach(entity);
        Context.Set<TEntity>().Update(entity);
    }

    public virtual async Task DeleteRangeAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        List<TEntity> entities = [];

        foreach (Guid id in ids)
        {
            TEntity? entity = await Context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (entity is not null)
            {
                entity.Inactivate();
                entities.Add(entity);
            }
        }

        Context.AttachRange(entities);
        Context.Set<TEntity>().UpdateRange(entities);
    }

    #endregion
}
