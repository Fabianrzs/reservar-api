using Authentications.Application.Abstractions.Services.Auth;

namespace Authentications.Application.UseCase.Authentications.Auth.SignIn;

/// <summary>
/// Handles sign-in using basic credentials (email and password).
/// </summary>
public class SignInCommandHandler(
    IUserRepository userRepository,
    IUserCredentialsRepository credentialsRepository,
    ISignInService signInService
) : ICommandHandler<SignInCommand, AuthenticationDto>
{
    public async Task<Result<AuthenticationDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email, AuthProvider.Local, cancellationToken);
        if (user is null)
        {
            return Result.Failure<AuthenticationDto>(AuthErrors.InvalidCredentials);
        }

        UserCredentials? credentials = await credentialsRepository.GetByUserIdAsync(user.Id, cancellationToken);
        
        if (credentials is null || !credentials.CurrentPassword.Verify(request.Password))
        {
            return Result.Failure<AuthenticationDto>(AuthErrors.InvalidCredentials);
        }

        return await signInService.SignInAsync(user, cancellationToken);
    }
}
