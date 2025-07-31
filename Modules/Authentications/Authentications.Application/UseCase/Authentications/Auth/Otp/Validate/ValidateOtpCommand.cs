namespace Authentications.Application.UseCase.Authentications.Auth.Otp.Validate;

/// <summary>
/// Command to validate an OTP token for a user.
/// </summary>
/// <param name="Email">User's email address.</param>
/// <param name="Token">The OTP token to validate.</param>
public record ValidateOtpCommand(
    string Email,
    string Token
) : ICommand<AuthenticationDto>;
