namespace Common.Application.Contexts;

public interface ISeeder<in TContext> where TContext : IAppDbContext
{
    Task SeedAsync(TContext context, CancellationToken cancellationToken = default);
}

