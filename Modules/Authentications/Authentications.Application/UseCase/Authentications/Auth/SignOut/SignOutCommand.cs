namespace Authentications.Application.UseCase.Authentications.Auth.SignOut;

/// <summary>
/// Represents a command to sign out a user by invalidating the active session.
/// </summary>
/// <param name="SessionId">The unique identifier of the session to invalidate.</param>
public record SignOutCommand(Guid SessionId) : ICommand;
