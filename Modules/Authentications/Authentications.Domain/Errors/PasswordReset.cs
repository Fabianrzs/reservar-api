namespace Authentications.Domain.Errors;

/// <summary>
/// Errors related to password reset operations.
/// </summary>
public static class PasswordResetErrors
{
    public static readonly Error TokenExpired = Error.Unauthorized(
        code: "PasswordReset.TokenExpired",
        description: "Password reset token has expired."
    );

    public static readonly Error TokenInvalid = Error.Unauthorized(
        code: "PasswordReset.TokenInvalid",
        description: "Password reset token is invalid."
    );
}
