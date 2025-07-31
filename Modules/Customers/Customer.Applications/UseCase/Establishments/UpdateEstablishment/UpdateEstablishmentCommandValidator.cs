namespace Customers.Application.UseCase.Establishments.UpdateEstablishment;

public class UpdateEstablishmentCommandValidator : AbstractValidator<UpdateEstablishmentCommand>
{
    public UpdateEstablishmentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Establishment.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Establishment.Description).NotEmpty().MaximumLength(500);

        RuleFor(x => x.Establishment.ContactInfo.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Establishment.ContactInfo.Email).EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Establishment.ContactInfo.Email));
        RuleFor(x => x.Establishment.ContactInfo.Website).MaximumLength(100);
        RuleFor(x => x.Establishment.ContactInfo.InstagramHandle).MaximumLength(100);
        RuleFor(x => x.Establishment.ContactInfo.FacebookHandle).MaximumLength(100);
        RuleFor(x => x.Establishment.ContactInfo.WhatsappNumber).MaximumLength(20);

        RuleForEach(x => x.Establishment.Branches).ChildRules(branch =>
        {
            branch.RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            branch.RuleFor(x => x.Address.Street).NotEmpty();
            branch.RuleFor(x => x.Address.Number).NotEmpty();
            branch.RuleFor(x => x.Address.City).NotEmpty();
            branch.RuleFor(x => x.Address.Country).NotEmpty();
            branch.RuleFor(x => x.Location.Latitude).InclusiveBetween(-90, 90);
            branch.RuleFor(x => x.Location.Longitude).InclusiveBetween(-180, 180);
        });
    }
}
