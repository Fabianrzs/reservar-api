using Authentications.Application.Abstractions.Services.Auth;
using Common.Application.Abstractions.Providers;
using Microsoft.AspNetCore.Http;

namespace Authentications.Infrastructure.Implementations.Services.Auth;

/// <summary>
/// Default implementation of <see cref="ISessionBuilder"/>.
/// Extracts contextual information from the HTTP request.
/// </summary>
public class SessionBuilder(
    IDateTimeProvider dateTimeProvider,
    IHttpContextAccessor httpContextAccessor
) : ISessionBuilder
{
    public Session Build(User user)
    {
        DateTime now = dateTimeProvider.UtcNow;

        HttpContext? context = httpContextAccessor.HttpContext;
        string ip = context?.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        string userAgent = context?.Request.Headers.UserAgent.ToString() ?? "unknown";

        return Session.Create(
            userId: user.Id,
            createdAt: now,
            expiresAt: now.AddDays(5),
            ip: ip,
            userAgent: userAgent
        );
    }
}
