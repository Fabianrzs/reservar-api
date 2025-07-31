using Authentications.Domain.Events.OTP;
using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Authentications.Auth.Otp.Request;

/// <summary>
/// Handles the creation of an OTP token for a user.
/// </summary>
public class RequestOtpCommandHandler(
    IUserRepository userRepository,
    IOtpTokenRepository otpTokenRepository,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<RequestOtpCommand>
{
    public async Task<Result> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email, AuthProvider.Local, cancellationToken);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        DateTime now = dateTimeProvider.UtcNow;
        DateTime expiration = now.AddMinutes(5);

        var otp = OtpToken.Create(user.Id, expiration);

        await otpTokenRepository.AddAsync(otp, cancellationToken);

        otp.Raise(new OtpRequestedDomainEvent(otp.Id, now));

        return Result.Success();
    }
}
