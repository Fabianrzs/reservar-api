using Common.Application.Abstractions.Providers;
using Common.Infrastructure.Configuration;
using Common.Infrastructure.Implementations.Builders;
using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure.Implementations.Providers;

public class ConnectionStringProvider(IConfiguration configuration) : IConnectionStringProvider
{
    public string Authentication =>
        new ConnectionStringBuilder(Base)
            .SetServer(configuration.GetValueOrThrow<string>("Authentications:Server"))
            .SetDatabase(configuration.GetValueOrThrow<string>("Authentications:Database"))
            .SetUser(configuration.GetValueOrThrow<string>("Authentications:User"))
            .SetPassword(configuration.GetValueOrThrow<string>("Authentications:Password"))
            .Build();

    public string Customer =>
        new ConnectionStringBuilder(Base)
            .SetServer(configuration.GetValueOrThrow<string>("Customers:Server"))
            .SetDatabase(configuration.GetValueOrThrow<string>("Customers:Database"))
            .SetUser(configuration.GetValueOrThrow<string>("Customers:User"))
            .SetPassword(configuration.GetValueOrThrow<string>("Customers:Password"))
            .Build();

    public string Base =>
        configuration.GetConnectionStringOrThrow("BaseConnection");
}
