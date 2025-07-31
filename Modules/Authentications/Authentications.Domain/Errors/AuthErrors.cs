namespace Authentications.Domain.Errors;

/// <summary>
/// Centralized authentication-related domain errors.
/// </summary>
public static class AuthErrors
{
    public static readonly Error InvalidCredentials = Error.Unauthorized(
        code: "Auth.InvalidCredentials",
        description: "Invalid email or password."
    );

    public static readonly Error UserNotFound = Error.NotFound(
        code: "Auth.UserNotFound",
        description: "User not found."
    );

    public static readonly Error UserInactive = Error.Forbidden(
        code: "Auth.UserInactive",
        description: "User is inactive."
    );

    public static readonly Error UnauthorizedAccess = Error.Unauthorized(
        code: "Auth.UnauthorizedAccess",
        description: "Access denied."
    );

    public static readonly Error EmailAlreadyRegistered = Error.Conflict(
        code: "Auth.EmailAlreadyRegistered",
        description: "Email is already registered."
    );

    public static readonly Error FederatedIdentityInvalid = Error.Unauthorized(
        code: "Auth.FederatedIdentityInvalid",
        description: "Unable to extract a valid email from the federated identity."
    );

    public static readonly Error OtpExpired = Error.Unauthorized(
        code: "Auth.OtpExpired",
        description: "The OTP token has expired or is invalid."
    );

    public static readonly Error SessionNotFound = Error.NotFound(
        code: "Auth.SessionNotFound",
        description: "The session does not exist or has already been terminated."
    );
}
