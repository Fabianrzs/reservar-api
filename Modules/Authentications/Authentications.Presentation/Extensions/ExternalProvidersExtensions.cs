using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Presentation.Extensions;

internal static class ExternalProvidersExtensions
{
    public static IServiceCollection AddGoogleProviderExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddGoogle(googleOptions =>
            {
                IConfigurationSection section = configuration.GetSection("Authentication:Google");
                googleOptions.ClientId = section["client_id"]!;
                googleOptions.ClientSecret = section["client_secret"]!;

            });
        return services;
    }
}

