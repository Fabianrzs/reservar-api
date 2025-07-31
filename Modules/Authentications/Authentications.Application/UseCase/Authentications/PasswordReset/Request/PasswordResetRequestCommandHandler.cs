using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Authentications.PasswordReset.Request;

public class PasswordResetRequestCommandHandler(
    IUserRepository userRepository,
    IPasswordResetTokenRepository tokenRepository,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<PasswordResetRequestCommand>
{
    public async Task<Result> Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
    {
        string email = request.Email?.Trim().ToUpperInvariant() ?? string.Empty;

        User? user = await userRepository.GetByEmailAsync(email, AuthProvider.Local, cancellationToken);
        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        DateTime now = dateTimeProvider.UtcNow;
        DateTime expiration = now.AddMinutes(10);

        var resetToken = PasswordResetToken.Create(user.Id, expiration);


        //Evento de dominio para enviar la informacion del token 

        await tokenRepository.AddAsync(resetToken, cancellationToken);

        return Result.Success();
    }
}
