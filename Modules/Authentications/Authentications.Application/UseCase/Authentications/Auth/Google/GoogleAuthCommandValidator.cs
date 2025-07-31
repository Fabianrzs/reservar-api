namespace Authentications.Application.UseCase.Authentications.Auth.Google;

public class GoogleAuthCommandValidator : AbstractValidator<GoogleAuthCommand>
{
    public GoogleAuthCommandValidator()
    {
        RuleFor(x => x.Principal)
            .NotNull().WithMessage("ClaimsPrincipal must not be null.");
    }
}
