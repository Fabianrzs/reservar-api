using Authentications.Application.Abstractions.Services.Auth;
using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Authentications.Auth.Otp.Validate;

/// <summary>
/// Handles the validation of an OTP token for a user.
/// </summary>
public class ValidateOtpCommandHandler(
    IUserRepository userRepository,
    IOtpTokenRepository otpTokenRepository,
    ISignInService signInService,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<ValidateOtpCommand, AuthenticationDto>
{
    public async Task<Result<AuthenticationDto>> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(
            request.Email,
            AuthProvider.Local,
            cancellationToken
        );

        if (user is null)
        {
            return Result.Failure<AuthenticationDto>(AuthErrors.UserNotFound);
        }

        bool isValid = await otpTokenRepository.IsValidTokenForUserAsync(
            userId: user.Id,
            token: request.Token,
            dateNow: dateTimeProvider.UtcNow,
            cancellationToken: cancellationToken
        );

        if (!isValid)
        {
            return Result.Failure<AuthenticationDto>(AuthErrors.OtpExpired);
        }

        AuthenticationDto authDto = await signInService.SignInAsync(user,cancellationToken);

        return Result.Success(authDto);
    }
}
