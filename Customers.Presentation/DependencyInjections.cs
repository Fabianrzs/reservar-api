using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Presentation;

public static class DependencyInjections
{
    public static IServiceCollection AddCustomersPresentations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(AssemblyReference.Assembly);
        return services;
    }
    public static WebApplication UseCustomersPresentations(this WebApplication app)
    {
        return app;
    }
}
