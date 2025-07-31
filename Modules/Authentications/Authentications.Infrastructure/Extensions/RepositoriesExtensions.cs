using Authentications.Infrastructure.Implementations.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Infrastructure.Extensions;

public static class RepositoriesExtensions
{
    /// <summary>
    /// Registers all authentication-related repositories for dependency injection.
    /// </summary>
    /// <param name="services">The service collection to register with.</param>
    public static IServiceCollection AddAuthenticationsRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IOtpTokenRepository, OtpTokenRepository>();
        services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

        return services;
    }
}
