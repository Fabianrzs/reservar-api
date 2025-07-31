namespace Authentications.Domain.Errors;

/// <summary>
/// Centralized errors related to OTP (One-Time Password) operations.
/// </summary>
public static class OtpErrors
{
    public static readonly Error OtpExpired = Error.Unauthorized(
        code: "Otp.Expired",
        description: "The OTP token has expired."
    );

    public static readonly Error OtpInvalid = Error.Unauthorized(
        code: "Otp.Invalid",
        description: "Invalid OTP token."
    );

    public static readonly Error OtpNotFound = Error.NotFound(
        code: "Otp.NotFound",
        description: "OTP token not found."
    );
}
