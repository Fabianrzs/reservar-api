namespace Authentications.Application.UseCase.Authentications.Auth.Otp.Validate;

public class ValidateOtpCommandValidator : AbstractValidator<ValidateOtpCommand>
{
    public ValidateOtpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("OTP token is required.")
            .Length(6).WithMessage("OTP token must be exactly 6 characters.")
            .Matches(@"^\d{6}$").WithMessage("OTP token must be numeric.");
    }
}
