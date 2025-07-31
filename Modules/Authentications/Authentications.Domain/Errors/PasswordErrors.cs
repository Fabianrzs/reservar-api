namespace Authentications.Domain.Errors;

/// <summary>
/// Centralized errors related to password operations.
/// </summary>
public static class PasswordErrors
{
    public static readonly Error PasswordExpired = Error.Unauthorized(
        code: "Password.Expired",
        description: "Your password has expired."
    );

    public static readonly Error PasswordPreviouslyUsed = Error.Unauthorized(
        code: "Auth.PasswordPreviouslyUsed",
        description: "Password was used recently. Please choose a new one."
    );
}
