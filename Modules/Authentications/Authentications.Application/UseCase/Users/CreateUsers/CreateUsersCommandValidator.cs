namespace Authentications.Application.UseCase.Users.CreateUsers;

public class CreateUsersCommandValidator : AbstractValidator<CreateUsersCommand>
{
    public CreateUsersCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));
    }
}
