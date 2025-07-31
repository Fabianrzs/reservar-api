using Authentications.Infrastructure.Extensions;
using Common.Infrastructure.Configuration;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Infrastructure;

/// <summary>
/// Central DI entry point for the Authentications module.
/// </summary>
public static class DependencyContainer
{
    /// <summary>
    /// Registers all infrastructure-layer services required for the Authentications module,
    /// including repositories, DbContext, unit of work, providers, and application services.
    /// </summary>
    /// <param name="services">The application's service collection.</param>
    /// <param name="configuration">The application's configuration, used to read connection strings and other settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with authentication services registered.</returns>
    public static IServiceCollection AddAuthenticationsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddConfigurationSection<JwtConfiguration>(configuration, "JwtConfiguration");

        services.AddPublishDomainEvent();

        services.AddAuthenticationInternal(configuration);

        services.AddAuthorizationInternal();

        services.AddAuthenticationsPersistence(configuration);

        services.AddAuthenticationsRepositories();

        services.AddAuthenticationServices();

        return services;
    }

    /// <summary>
    /// Executes runtime initialization for the Authentications module,
    /// such as database seeding or migrations during application startup.
    /// </summary>
    /// <param name="app">The current application builder.</param>
    /// <returns>The modified <see cref="IApplicationBuilder"/> after applying initialization logic.</returns>
    public static IApplicationBuilder UseAuthenticationsInfrastructure(this IApplicationBuilder app)
    {
        // Seed roles, permissions, default users, etc.
        app.UseAuthenticationsDbInitialization();

        return app;
    }
}
