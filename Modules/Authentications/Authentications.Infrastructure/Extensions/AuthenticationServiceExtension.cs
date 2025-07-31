using Authentications.Application.Abstractions.Services.Auth;
using Authentications.Infrastructure.Implementations.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Infrastructure.Extensions;

/// <summary>
/// Registers all authentication-related services.
/// </summary>
public static class AuthenticationServiceExtension
{
    /// <summary>
    /// Adds authentication application services to the DI container.
    /// </summary>
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<ISignInService, SignInService>();
        services.AddScoped<ISessionBuilder, SessionBuilder>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        return services;
    }
}
