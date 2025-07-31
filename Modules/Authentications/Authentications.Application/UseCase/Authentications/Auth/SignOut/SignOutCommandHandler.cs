namespace Authentications.Application.UseCase.Authentications.Auth.SignOut;

/// <summary>
/// Handles the sign-out process by invalidating a user session.
/// </summary>
public class SignOutCommandHandler(
    ISessionRepository sessionRepository
) : ICommandHandler<SignOutCommand>
{
    public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await sessionRepository.InactivateAsync(request.SessionId, cancellationToken);
        return Result.Success();
    }
}
