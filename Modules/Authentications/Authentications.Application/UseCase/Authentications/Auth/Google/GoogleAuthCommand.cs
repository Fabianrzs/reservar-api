using System.Security.Claims;

namespace Authentications.Application.UseCase.Authentications.Auth.Google;

/// <summary>
/// Command to authenticate a user with Google OAuth.
/// </summary>
/// <param name="Principal">The authenticated claims principal from Google.</param>
public record GoogleAuthCommand(ClaimsPrincipal Principal) : ICommand<AuthenticationDto>;
