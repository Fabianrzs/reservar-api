namespace Authentications.Application.UseCase.Authentications.PasswordReset.Validate;

public record ValidatePasswordResetTokenCommand(
string Email,
string Token
) : ICommand<string>;
