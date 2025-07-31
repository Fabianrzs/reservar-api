using Authentications.Application.UseCase.Authentications;

namespace Authentications.Application.Abstractions.Services.Auth;

public interface ISignInService
{
    Task<AuthenticationDto> SignInAsync(User user, CancellationToken cancellationToken);
}
