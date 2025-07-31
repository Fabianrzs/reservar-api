using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Infrastructure.Extensions;

/// <summary>
/// Internal helper for applying includes to an IQueryable.
/// </summary>
internal static class IncludeExtensions
{
    public static IQueryable<TEntity> ApplyIncludes<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, object>>[]? includes)
        where TEntity : class
    {
        if (includes == null)  {  return query; }

        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
