using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Presentation.Extensions;

public static class GlobalizationExtensions
{
    public static IServiceCollection AddGlobalization(this IServiceCollection services)
    {
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            CultureInfo[] supportedCultures =
            [
                new CultureInfo("en"),
                new CultureInfo("es"),
            ];

            options.DefaultRequestCulture = new RequestCulture("es");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders =
            [
                new AcceptLanguageHeaderRequestCultureProvider()
            ];
        });

        return services;
    }

    public static WebApplication UseGlobalization(this WebApplication app)
    {
        RequestLocalizationOptions localizationOptions = 
            app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}
