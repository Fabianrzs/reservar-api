using Authentications.Infrastructure.Implementations.Persistence;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Implementations.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods to configure the persistence layer of the Authentications module.
/// </summary>
public static class PersistenceExtension
{
    /// <summary>
    /// Registers the authentication database context, repositories, unit of work, and seeders.
    /// </summary>
    /// <param name="services">The service collection to register components into.</param>
    /// <param name="configuration">The application's configuration source.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAuthenticationsPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = new ConnectionStringProvider(configuration);

        // Get the connection string for the authentication module.
        string defaultConnectionString = provider.Authentication;

        // Register Unit of Work implementation for managing atomic operations.
        services.AddUnitOfWork();

        // Register the authentication DbContext in the DI container for usage across the module.
        services.AddContexts<AuthenticationDbContext>();

        // Register the actual EF Core DbContext with SQL Server and custom migration history table.
        services.AddDbContexts<AuthenticationDbContext>(options =>
            options.UseSqlServer(defaultConnectionString, msqlOptions =>
                msqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName)));

        // Register seeders to apply default data during initialization.
        services.AddSeeders<AuthenticationDbContext>();

        return services;
    }

    /// <summary>
    /// Applies pending migrations and executes seeders during application startup.
    /// </summary>
    /// <param name="app">The current <see cref="WebApplication"/> instance.</param>
    /// <returns>The updated <see cref="WebApplication"/>.</returns>
    public static IApplicationBuilder UseAuthenticationsDbInitialization(this IApplicationBuilder app)
    {
        app.ApplySeeders<AuthenticationDbContext>();
        return app;
    }
}
