namespace Authentications.Application.UseCase.Authentications.Auth.SignUp;

public sealed record SignUpCommand(
    string Email,
    string Password,
    string? Name
) : ICommand;
