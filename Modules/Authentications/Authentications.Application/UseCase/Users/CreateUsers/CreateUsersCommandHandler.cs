using Common.Application.Abstractions.Providers;

namespace Authentications.Application.UseCase.Users.CreateUsers;

public class CreateUsersCommandHandler(
    IUserRepository userRepository,
    IUserCredentialsRepository credentialsRepository,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CreateUsersCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
    {
        string email = request.Email?.Trim().ToUpperInvariant() ?? string.Empty;

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
        {
            return Result.Failure<Guid>(AuthErrors.EmailAlreadyRegistered);
        }

        var user = User.Create(email, AuthProvider.Local, request.Name);

        (UserCredentials credentials, string plainPassword) = UserCredentials.CreateWithGeneratedPassword(
            user.Id,
            dateTimeProvider.UtcNow
        );

        await userRepository.AddAsync(user, cancellationToken);
        await credentialsRepository.AddAsync(credentials, cancellationToken);


        user.Raise(new UserCreatedWithPasswordDomainEvent(
            user.Id,
            user.Email,
            plainPassword,
            dateTimeProvider.UtcNow
        ));

        return Result.Success(user.Id);
    }
}
