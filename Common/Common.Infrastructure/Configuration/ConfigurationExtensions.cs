using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Obtiene una cadena de conexión o lanza una excepción si no existe.
    /// </summary>
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Connection string name cannot be null or empty.", nameof(name));
        }

        return configuration.GetConnectionString(name)
            ?? throw new InvalidOperationException($"Connection string '{name}' was not found in configuration.");
    }

    /// <summary>
    /// Obtiene un valor fuertemente tipado desde la configuración o lanza si no existe o es inválido.
    /// </summary>
    public static T GetValueOrThrow<T>(this IConfiguration configuration, string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Configuration key cannot be null or empty.", nameof(key));
        }

        return configuration.GetValue<T?>(key) ?? throw new InvalidOperationException(
            $"Configuration value for key '{key}' was not found or is invalid.");
    }

    /// <summary>
    /// Obtiene una sección de configuración fuertemente tipada o lanza si no existe.
    /// </summary>
    public static T GetSectionOrThrow<T>(this IConfiguration configuration, string sectionName)
        where T : class, new()
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            throw new ArgumentException("Section name cannot be null or empty.", nameof(sectionName));
        }

        return configuration.GetSection(sectionName).Get<T>() ?? throw new InvalidOperationException(
            $"Configuration section '{sectionName}' of type '{typeof(T).Name}' was not found or is invalid.");
    }

    /// <summary>
    /// Configura una sección de configuración y la registra en el contenedor.
    /// </summary>
    public static IServiceCollection AddConfigurationSection<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
        where T : class
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            throw new ArgumentException("Section name cannot be null or empty.", nameof(sectionName));
        }

        services.Configure<T>(configuration.GetSection(sectionName));
        return services;
    }
}
