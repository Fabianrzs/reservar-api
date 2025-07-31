namespace Customers.Application.UseCase.Branches;

public class BranchMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BranchDto, Branch>()
            .ConstructUsing(dto => Branch.Create(
                dto.Name,
                dto.EstablishmentId,
                dto.Address.Adapt<Address>(),
                dto.Location.Adapt<Location>()
            ));
    }
}
