using Common.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Infrastructure.Extensions;

public static class PublisherDomainEventExtensions
{
    public static IServiceCollection AddPublishDomainEvent(this IServiceCollection services)
    {
        services.TryAddSingleton<PublishDomainEventsInterceptor>();
        return services;
    }
}
