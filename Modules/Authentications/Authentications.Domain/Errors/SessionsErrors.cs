namespace Authentications.Domain.Errors;

/// <summary>
/// Errors related to user sessions.
/// </summary>
public static class SessionsErrors
{
    public static readonly Error SessionExpired = Error.Unauthorized(
        code: "Sessions.Expired",
        description: "The session has expired."
    );

    public static readonly Error SessionRevoked = Error.Unauthorized(
        code: "Sessions.Revoked",
        description: "The session has been revoked."
    );

    public static readonly Error SessionNotFound = Error.NotFound(
        code: "Sessions.NotFound",
        description: "Session not found."
    );
}
