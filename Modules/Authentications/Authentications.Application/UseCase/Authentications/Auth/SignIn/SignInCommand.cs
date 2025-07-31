namespace Authentications.Application.UseCase.Authentications.Auth.SignIn;


public record SignInCommand(string Email, string Password) : ICommand<AuthenticationDto>;
