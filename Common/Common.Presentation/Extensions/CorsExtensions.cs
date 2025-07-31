using Authentications.Presentation.Commons;
using Common.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Common.Presentation.Extensions;
public static class CorsExtensions
{
    private const string CorsPolicyName = "CustomCorsPolicy";

    /// <summary>
    /// Configura CORS a partir de la configuración en appsettings.json.
    /// </summary>
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        CorsOptionsConfig corsSettings = configuration.GetSectionOrThrow<CorsOptionsConfig>("CorsSettings");

        if (corsSettings != null && corsSettings.EnableCors)
        {
            services.AddCors(options => options.AddPolicy(CorsPolicyName, builder =>
                {
                    if (corsSettings.AllowAllOrigins)
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                    else
                    {
                        builder.WithOrigins(corsSettings.AllowedOrigins ?? [])
                               .WithMethods(corsSettings.AllowedMethods ?? [])
                               .WithHeaders(corsSettings.AllowedHeaders ?? []);

                        if (corsSettings.AllowCredentials)
                        {
                            builder.AllowCredentials();
                        }
                    }
                }));
        }

        return services;
    }

    /// <summary>
    /// Aplica la configuración de CORS a la aplicación.
    /// </summary>
    public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }
}

