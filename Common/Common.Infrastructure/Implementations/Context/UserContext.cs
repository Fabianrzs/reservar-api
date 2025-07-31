using System.Security.Claims;
using Common.Application.Abstractions.Context;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Implementations.Context;

public class UserContext : IUserContext
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity is not { IsAuthenticated: true })
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        Id = GetClaimValueAsGuid(user, ClaimTypes.NameIdentifier);
        SessionId = GetClaimValueAsGuid(user, "SessionId");
        Email = GetClaimValue(user, ClaimTypes.Email) ?? string.Empty;
        Name = GetClaimValue(user, ClaimTypes.Name) ?? string.Empty;
    }

    private static string? GetClaimValue(ClaimsPrincipal user, string claimType)
        => user.FindFirst(claimType)?.Value;

    private static Guid GetClaimValueAsGuid(ClaimsPrincipal user, string claimType)
    {
        string? value = GetClaimValue(user, claimType);
        return Guid.TryParse(value, out Guid guid)
            ? guid
            : throw new UnauthorizedAccessException($"Missing or invalid claim: {claimType}");
    }
}
