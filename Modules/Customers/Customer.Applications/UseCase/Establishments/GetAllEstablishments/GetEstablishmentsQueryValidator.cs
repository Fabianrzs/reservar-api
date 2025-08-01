namespace Customers.Application.UseCase.Establishments.GetAllEstablishments;

public class GetEstablishmentsQueryValidator : AbstractValidator<GetEstablishmentsQuery>
{
    public GetEstablishmentsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
