using System.Linq.Expressions;

namespace Common.Domain;

/// <summary>
/// Defines a generic contract for repository operations for a given entity type.
/// Supports asynchronous CRUD operations with support for includes.
/// </summary>
/// <typeparam name="TEntity">The type of the entity, derived from <see cref="Entity"/>.</typeparam>
public interface IRepository<TEntity> where TEntity : Entity
{

    #region Read Operations

    /// <summary>
    /// Retrieves a paginated list of entities matching the optional filter, with optional includes and ordering.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="predicate">An optional expression to filter the entities.</param>
    /// <param name="includes">Optional navigation properties to include in the query.</param>
    /// <param name="orderBy">An optional function to order the query results.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A paged result containing the list of entities and pagination metadata.</returns>
    Task<PagedResult<TEntity>> PaginateAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Determines whether any entities match the specified predicate.
    /// </summary>
    /// <param name="predicate">The expression to filter entities.</param>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task<TEntity?> GetByIdAsync(
        Guid id,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities of the specified type.
    /// </summary>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">The expression to filter entities.</param>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the first entity that matches the given predicate.
    /// </summary>
    /// <param name="predicate">The expression to filter entities.</param>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task<TEntity> FindFirstAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the number of entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">The expression to filter entities.</param>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default);

    #endregion

    #region Create Operations

    /// <summary>
    /// Adds a single entity to the context.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple entities to the context.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    #endregion

    #region Update Operations

    /// <summary>
    /// Updates an existing entity in the context.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities in the context.
    /// </summary>
    /// <param name="entities">The collection of entities to update.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    #endregion

    #region Delete Operations

    /// <summary>
    /// Soft-deletes a single entity by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the entity to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft-deletes multiple entities by their IDs.
    /// </summary>
    /// <param name="ids">The collection of IDs for the entities to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task DeleteRangeAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default);

    #endregion
}
