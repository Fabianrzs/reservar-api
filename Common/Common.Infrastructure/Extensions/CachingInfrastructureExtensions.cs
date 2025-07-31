using Common.Application.Caching;
using Common.Infrastructure.Caching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Common.Infrastructure.Extensions;

public static class CachingInfrastructureExtensions
{
    public static IServiceCollection AddCachingInfrastructure(this IServiceCollection services, string redisConnectionString)
    {
        try
        {
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(redis);
            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = 
                    () => Task.FromResult<IConnectionMultiplexer>(redis));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();
        return services;
    }
}
