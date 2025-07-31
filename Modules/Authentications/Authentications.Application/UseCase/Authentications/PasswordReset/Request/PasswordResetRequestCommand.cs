namespace Authentications.Application.UseCase.Authentications.PasswordReset.Request;

public sealed record PasswordResetRequestCommand(string Email) : ICommand;
