using Common.Application.Contexts;

namespace Common.Infrastructure.Contexts;

public class SeederManager<TContext>(IEnumerable<ISeeder<TContext>> seeders) where TContext : AppDbContextBase
{
    public async Task SeedAsync(TContext context, CancellationToken cancellationToken = default)
    {
        foreach (ISeeder<TContext> seeder in seeders)
        {
            await seeder.SeedAsync(context, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
