using Common.Infrastructure.Extensions;
using Common.Infrastructure.Implementations.Providers;
using Customers.Infrastructure.Implementations.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods to configure the persistence layer of the Customers module.
/// </summary>
public static class PersistenceExtension
{
    /// <summary>
    /// Registers the Customers database context, repositories, unit of work, and seeders.
    /// </summary>
    /// <param name="services">The service collection to register components into.</param>
    /// <param name="configuration">The application's configuration source.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCustomersPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = new ConnectionStringProvider(configuration);

        // Get the connection string for the Customers module.
        string connectionString = provider.Customer;

        // Register Unit of Work implementation for managing atomic operations.
        services.AddUnitOfWork();

        // Register the Customers DbContext in the DI container.
        services.AddContexts<CustomersDbContext>();

        // Register the EF Core DbContext with SQL Server and a custom migration history table.
        services.AddDbContexts<CustomersDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName)));

        // Register seeders for initial data.
        services.AddSeeders<CustomersDbContext>();

        return services;
    }

    /// <summary>
    /// Applies pending migrations and executes seeders during application startup.
    /// </summary>
    /// <param name="app">The current <see cref="WebApplication"/> instance.</param>
    /// <returns>The updated <see cref="WebApplication"/>.</returns>
    public static IApplicationBuilder UseCustomersDbInitialization(this IApplicationBuilder app)
    {
        app.ApplySeeders<CustomersDbContext>();
        return app;
    }
}
