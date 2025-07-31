using Authentications.Application.Abstractions.Services.Auth;
using Authentications.Application.UseCase.Authentications;

namespace Authentications.Infrastructure.Implementations.Services.Auth;
public class RefreshTokenService(
    ISessionRepository sessionRepository,
    ISessionBuilder sessionBuilder,
    ITokenProvider tokenProvider
    ) : IRefreshTokenService
{
    public async Task<RefreshTokenDto> RefreshTokenAsync(User user, Guid sessionId, CancellationToken cancellationToken)
    {
        await sessionRepository.InactivateAsync(sessionId, cancellationToken);   

        Session session = sessionBuilder.Build(user);

        await sessionRepository.AddAsync(session, cancellationToken);

        string accessToken = tokenProvider.GenerateAccessToken(session.Id, user);

        string refreshToken = tokenProvider.GenerateRefreshToken(session.Id, user.Id);

        return new RefreshTokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }
}
