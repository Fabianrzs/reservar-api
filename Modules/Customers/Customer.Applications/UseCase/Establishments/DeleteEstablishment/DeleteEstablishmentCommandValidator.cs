namespace Customers.Application.UseCase.Establishments.DeleteEstablishment;

public class DeleteEstablishmentCommandValidator : AbstractValidator<DeleteEstablishmentCommand>
{
    public DeleteEstablishmentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
