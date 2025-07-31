namespace Authentications.Application.UseCase.Authentications.Auth.Otp.Request;

/// <summary>
/// Command to request an OTP (One-Time Password) based on email.
/// </summary>
/// <param name="Email">The email of the user.</param>
public record RequestOtpCommand(string Email) : ICommand;
