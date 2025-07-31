using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Authentications.PasswordReset.Validate;

/// <summary>
/// Handles the validation of a password reset token.
/// </summary>
public class ValidatePasswordResetTokenCommandHandler(
    IUserRepository userRepository,
    IPasswordResetTokenRepository tokenRepository,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<ValidatePasswordResetTokenCommand, string>
{
    public async Task<Result<string>> Handle(ValidatePasswordResetTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email, 
            AuthProvider.Local, cancellationToken);
        if (user is null)
        {
            return Result.Failure<string>(AuthErrors.UserNotFound);
        }

        bool token = await tokenRepository.IsValidTokenAsync(user.Id, request.Token, 
            dateTimeProvider.UtcNow, cancellationToken);
        
        if (token)
        {
            return Result.Failure<string>(PasswordResetErrors.TokenInvalid);
        }
        string accessToken = "";
        return accessToken;
    }
}
