using Common.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Authentications.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddAuthenticationsApplication(this IServiceCollection services)
    {
        services.AddApplication([AssemblyReference.Assembly]);
        return services;
    }
}
