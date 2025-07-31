using Customers.Infrastructure.Implementations.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Infrastructure.Extensions;

/// <summary>
/// Registers all customer-related repositories for dependency injection.
/// </summary>
public static class RepositoriesExtensions
{
    /// <summary>
    /// Registers repositories from the Customers module.
    /// </summary>
    /// <param name="services">The service collection to register with.</param>
    public static IServiceCollection AddCustomersRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IEstablishmentUserRepository, EstablishmentUserRepository>();

        return services;
    }
}
