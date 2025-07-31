using Microsoft.Extensions.DependencyInjection;

namespace Customers.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
    {
        services.AddApplication([AssemblyReference.Assembly]);
        return services;
    }
}
