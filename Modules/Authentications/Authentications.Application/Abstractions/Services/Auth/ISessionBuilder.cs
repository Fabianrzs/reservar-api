namespace Authentications.Application.Abstractions.Services.Auth;

/// <summary>
/// Responsible for constructing new user sessions.
/// </summary>
public interface ISessionBuilder
{
    /// <summary>
    /// Builds a new session for the given user.
    /// </summary>
    /// <param name="user">The authenticated user.</param>
    /// <returns>A fully constructed session entity.</returns>
    Session Build(User user);
}
