using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Presentation;

public static class DependencyInjections
{
    public static IServiceCollection AddAuthenticationsPresentations(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddEndpoints(AssemblyReference.Assembly);
        return services;
    }
    public static WebApplication UseAuthenticationsPresentations(this WebApplication app)
    {
        return app;
    }
}
