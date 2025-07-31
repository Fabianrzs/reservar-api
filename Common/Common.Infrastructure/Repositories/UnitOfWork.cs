using Common.Application.Contexts;
using Common.Domain;
using Common.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Infrastructure.Repositories;

public class UnitOfWork(IAppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? currentTransaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction != null) { return; } 
        var dbContext = (AppDbContextBase)context;
        currentTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction == null)
        {
            throw new InvalidOperationException("No active transaction.");
        }

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            await currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction != null)
        {
            await currentTransaction.RollbackAsync(cancellationToken);
            await DisposeTransactionAsync();
        }
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    private async Task DisposeTransactionAsync()
    {
        if (currentTransaction != null)
        {
            await currentTransaction.DisposeAsync();
            currentTransaction = null;
        }
    }

    public void Dispose()
    {
        currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}
