using Customers.Application.UseCase.Branches;

namespace Customers.Application.UseCase.Establishments;

public class EstablishmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EstablishmentDto, Establishment>()
            .ConstructUsing(src => Establishment.Create(
                src.Name,
                src.Description,
                src.ContactInfo.Adapt<ContactInfo>()
            )).AfterMapping((src, dest) =>
            {
                foreach (BranchDto branchDto in src.Branches!)
                {
                    Branch branch = branchDto.Adapt<Branch>();
                    dest.AddBranch(branch);
                }
            });
    }
}
