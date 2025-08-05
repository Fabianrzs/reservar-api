using Authentications.Application.Abstractions.Services.Auth;
using Common.Application.Abstractions.Context;

namespace Authentications.Application.UseCase.Authentications.Auth.RefreshToken;

/// <summary>
/// Handles sign-in using basic credentials (email and password).
/// </summary>
public class RefreshTokenCommandHandler(
    IUserContext userContext,
    IUserRepository userRepository,
    IRefreshTokenService refreshTokenService
) : ICommandHandler<RefreshTokenCommand, RefreshTokenDto>
{
    public async Task<Result<RefreshTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(userContext.Id, cancellationToken: cancellationToken);

        if (user is null)
        {
            return Result.Failure<RefreshTokenDto>(AuthErrors.SessionNotFound);
        }

        return await refreshTokenService.RefreshTokenAsync(user, userContext.SessionId, cancellationToken);
    }
}
