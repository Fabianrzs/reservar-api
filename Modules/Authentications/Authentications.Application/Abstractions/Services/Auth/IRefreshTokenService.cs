using Authentications.Application.UseCase.Authentications;

namespace Authentications.Application.Abstractions.Services.Auth;

public interface IRefreshTokenService
{
    Task<RefreshTokenDto> RefreshTokenAsync(User user, Guid sessionId, CancellationToken cancellationToken);
}
