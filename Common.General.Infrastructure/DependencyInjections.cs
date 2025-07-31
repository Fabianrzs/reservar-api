using Authentications.Application;
using Authentications.Infrastructure;
using Authentications.Presentation;
using Common.Application.Abstractions.Context;
using Common.Application.Abstractions.Providers;
using Common.Infrastructure.Implementations.Context;
using Common.Infrastructure.Implementations.Providers;
using Common.Presentation.Extensions;
using Customers.Application;
using Customers.Infrastructure;
using Customers.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.General.Infrastructure;

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddGeneralInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {


        ArgumentNullException.ThrowIfNull(configuration);

        services.AddHttpContextAccessor();

        services.AddSwaggerConfiguration();

        services.AddGlobalization();

        services.AddCorsConfiguration(configuration);

        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
        services.AddScoped<IUserContext, UserContext>();

        services.AddVersioning();


        #region Modules

        //Authentications
        services.AddAuthenticationsInfrastructure(configuration);
        services.AddAuthenticationsApplication();
        services.AddAuthenticationsPresentations(configuration);


        //Customers
        services.AddCustomersInfrastructure(configuration);
        services.AddCustomersApplication();
        services.AddCustomersPresentations(configuration);

        #endregion


        return services;
    }

    public static WebApplication UseGeneralInfrastructure(this WebApplication app)
    {
        app.UseCustomSwagger();

        app.UseGlobalization();

        app.UseCorsConfiguration();

        app.UseMapVersionedMinimalApiEndpoints();

        #region Modules
        app.UseAuthenticationsInfrastructure();
        app.UseAuthenticationsPresentations();

        app.UseCustomersInfrastructure();
        app.UseCustomersPresentations();
        #endregion

        return app;
    }
}
