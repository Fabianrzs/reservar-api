using Common.Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Extensions;

public static class SeederExtensions
{
    public static async Task RunSeedersAsync<TContext>(this IServiceProvider serviceProvider)
        where TContext : AppDbContextBase
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        TContext context = scope.ServiceProvider.GetRequiredService<TContext>();
        SeederManager<TContext> seederManager = scope.ServiceProvider.GetRequiredService<SeederManager<TContext>>();

        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await seederManager.SeedAsync(context);
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
