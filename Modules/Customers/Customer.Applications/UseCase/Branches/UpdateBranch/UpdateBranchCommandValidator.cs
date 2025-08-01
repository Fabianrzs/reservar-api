namespace Customers.Application.UseCase.Branches.UpdateBranch;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Branch.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Branch.Address.Street).NotEmpty();
        RuleFor(x => x.Branch.Address.Number).NotEmpty();
        RuleFor(x => x.Branch.Address.City).NotEmpty();
        RuleFor(x => x.Branch.Address.Country).NotEmpty();
        RuleFor(x => x.Branch.Location.Latitude).InclusiveBetween(-90, 90);
        RuleFor(x => x.Branch.Location.Longitude).InclusiveBetween(-180, 180);
    }
}
