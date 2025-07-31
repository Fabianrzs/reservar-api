using System.Security.Claims;
using Authentications.Application.Abstractions.Services.Auth;

namespace Authentications.Application.UseCase.Authentications.Auth.Google;
/// <summary>
/// Handles authentication using Google OAuth identity provider.
/// If the user does not exist, it is created; if it exists, it reuses the user and generates a new session.
/// </summary>
public class GoogleAuthCommandHandler(
    IUserRepository userRepository, 
    ISignInService signInService
) : ICommandHandler<GoogleAuthCommand, AuthenticationDto>
{
    public async Task<Result<AuthenticationDto>> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal principal = request.Principal;

        string? email = principal.FindFirst(ClaimTypes.Email)?.Value;
        string? name = principal.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<AuthenticationDto>(AuthErrors.FederatedIdentityInvalid);
        }

        User? user = await userRepository.GetByEmailAsync(email, AuthProvider.Google, cancellationToken)
                           ?? User.Create(email, AuthProvider.Google, name);
        
        if (user.Id == Guid.Empty)
        {
            await userRepository.AddAsync(user, cancellationToken);
        }

        AuthenticationDto authDto = await signInService.SignInAsync(user, cancellationToken);
        return authDto;
    }
}
