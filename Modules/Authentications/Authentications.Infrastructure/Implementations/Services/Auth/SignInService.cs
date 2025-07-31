using Authentications.Application.Abstractions.Services.Auth;
using Authentications.Application.UseCase.Authentications;
using Authentications.Domain.Events.Users;

namespace Authentications.Infrastructure.Implementations.Services.Auth;

/// <summary>
/// Service responsible for signing in users and registering sessions.
/// </summary>
public class SignInService(
    ISessionRepository sessionRepository,
    ISessionBuilder sessionBuilder,
    ITokenProvider tokenProvider
) : ISignInService
{
    /// <inheritdoc />
    public async Task<AuthenticationDto> SignInAsync(
        User user,
        CancellationToken cancellationToken)
    {
        Session session = sessionBuilder.Build(user);

        await sessionRepository.AddAsync(session, cancellationToken);

        user.Raise(new UserSignedInDomainEvent(user.Id, session.CreatedAt));

        string accessToken = tokenProvider.GenerateAccessToken(session.Id, user);

        string refreshToken = tokenProvider.GenerateRefreshToken(session.Id, user.Id);

        return new AuthenticationDto{
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            AccessToken = accessToken,
            RefreshToken  = refreshToken,
        };
    }
}
