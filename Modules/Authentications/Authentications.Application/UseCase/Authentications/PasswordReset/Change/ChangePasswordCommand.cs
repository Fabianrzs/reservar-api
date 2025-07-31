namespace Authentications.Application.UseCase.Authentications.PasswordReset.Change;

public record ChangePasswordCommand(string NewPassword) : ICommand<AuthenticationDto>;
