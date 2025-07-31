using Authentications.Domain.ValueObjects;
using Common.Application.Abstractions.Context;
using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Authentications.PasswordReset.Change;

/// <summary>
/// Handles password change using a valid reset token.
/// </summary>
public class ChangePasswordCommandHandler(
    IUserCredentialsRepository credentialsRepository,
    IDateTimeProvider dateTimeProvider, IUserContext userContext
) : ICommandHandler<ChangePasswordCommand, AuthenticationDto>
{
    public async Task<Result<AuthenticationDto>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        UserCredentials? credentials = await credentialsRepository.GetByUserIdAsync(userContext.Id, cancellationToken);

        if (credentials is null)
        {
            return Result.Failure<AuthenticationDto>(AuthErrors.UserNotFound);
        }

        var newHashed = HashedPassword.HashPassword(request.NewPassword);

        if (credentials.CurrentPassword == newHashed)
        {
            return Result.Failure<AuthenticationDto>(PasswordErrors.PasswordPreviouslyUsed);
        }

        if (credentials.WasUsedBefore(newHashed))
        {
            return Result.Failure<AuthenticationDto>(PasswordErrors.PasswordPreviouslyUsed);
        }

        credentials.RotatePassword(newHashed, dateTimeProvider.UtcNow);
        await credentialsRepository.UpdateAsync(credentials, cancellationToken);

        return Result.Success<AuthenticationDto>(new());
    }
}
