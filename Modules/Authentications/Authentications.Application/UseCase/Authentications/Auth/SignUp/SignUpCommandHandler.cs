using Authentications.Domain.ValueObjects;
using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Authentications.Auth.SignUp;

/// <summary>
/// Handles the creation of a new local user account.
/// </summary>
public class SignUpCommandHandler(
    IUserRepository userRepository,
    IUserCredentialsRepository credentialsRepository,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<SignUpCommand>
{
    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        string email = request.Email?.Trim().ToUpperInvariant() ?? string.Empty;

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
        {
            return Result.Failure(AuthErrors.EmailAlreadyRegistered);
        }

        var user = User.Create(email, AuthProvider.Local, request.Name);

        var password = HashedPassword.HashPassword(request.Password);

        var credentials = UserCredentials.Create(
            userId: user.Id,
            password: password,
            passwordSetAt: dateTimeProvider.UtcNow
        );

        
        //Generar evento de dominio para confirma el resgistro

        await userRepository.AddAsync(user, cancellationToken);
        await credentialsRepository.AddAsync(credentials, cancellationToken);

        return Result.Success();
    }
}
