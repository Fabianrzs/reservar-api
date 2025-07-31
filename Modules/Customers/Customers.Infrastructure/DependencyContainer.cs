using Common.Infrastructure.Extensions;
using Customers.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Infrastructure;

/// <summary>
/// Central DI entry point for the Customers module.
/// </summary>
public static class DependencyContainer
{
    /// <summary>
    /// Registers all infrastructure-layer services required for the Customers module,
    /// including repositories, DbContext, unit of work, providers, and application services.
    /// </summary>
    /// <param name="services">The application's service collection.</param>
    /// <param name="configuration">The application's configuration, used to read connection strings and other settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with customer services registered.</returns>
    public static IServiceCollection AddCustomersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddPublishDomainEvent();

        services.AddCustomersPersistence(configuration);

        services.AddCustomersRepositories();

        return services;
    }

    /// <summary>
    /// Executes runtime initialization for the Customers module,
    /// such as database seeding or migrations during application startup.
    /// </summary>
    /// <param name="app">The current application builder.</param>
    /// <returns>The modified <see cref="IApplicationBuilder"/> after applying initialization logic.</returns>
    public static IApplicationBuilder UseCustomersInfrastructure(this IApplicationBuilder app)
    {
        app.UseCustomersDbInitialization();

        return app;
    }
}
