using Common.Application.Contexts;
using Common.Domain;
using Common.Infrastructure.Contexts;
using Common.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddDbContexts<TAppDbContext>(this IServiceCollection services)
        where TAppDbContext : AppDbContextBase => services.AddDbContext<TAppDbContext>();

    public static IServiceCollection AddDbContexts<TAppDbContext>(
            this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction)
        where TAppDbContext : AppDbContextBase => services.AddDbContext<TAppDbContext>(optionsAction);

    public static IServiceCollection AddDbContextsOptions<TAppDbContext>(
            this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped)
        where TAppDbContext : AppDbContextBase
    {
        services.AddDbContext<TAppDbContext>(optionsAction, contextLifetime);
        return services;
    }

    public static IServiceCollection AddDbContextsWithFactory<TAppDbContext>(
        this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null,
        ServiceLifetime factoryLifetime = ServiceLifetime.Scoped)
        where TAppDbContext : AppDbContextBase
    {
        services.AddDbContextFactory<TAppDbContext>(optionsAction, factoryLifetime);
        return services;
    }

    public static IServiceCollection AddContexts<TAppDbContext>(this IServiceCollection services)
        where TAppDbContext : AppDbContextBase
    {
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<TAppDbContext>());
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddSeeders<TContext>(this IServiceCollection services)
    where TContext : AppDbContextBase
    {
        services.Scan(scan => scan.FromAssemblies(typeof(TContext).Assembly)
            .AddClasses(c => c.AssignableTo<ISeeder<TContext>>())
            .AsImplementedInterfaces().WithScopedLifetime());

        services.AddScoped<SeederManager<TContext>>();

        return services;
    }

    public static IApplicationBuilder ApplyMigrations<TAppDbContext>(this IApplicationBuilder applications)
        where TAppDbContext : AppDbContextBase
    {
        using IServiceScope scope = applications.ApplicationServices.CreateScope();
        using TAppDbContext dbContext = scope.ServiceProvider.GetRequiredService<TAppDbContext>();
        dbContext.Database.Migrate();
        return applications;
    }
    public static IApplicationBuilder ApplySeeders<TAppDbContext>(this IApplicationBuilder applications)
    where TAppDbContext : AppDbContextBase
    {
        using IServiceScope scope = applications.ApplicationServices.CreateScope();
        using TAppDbContext dbContext = scope.ServiceProvider.GetRequiredService<TAppDbContext>();
        SeederManager<TAppDbContext> seederManager = scope.ServiceProvider.GetRequiredService<SeederManager<TAppDbContext>>();

        seederManager.SeedAsync(dbContext).GetAwaiter().GetResult();

        return applications;
    }
}
