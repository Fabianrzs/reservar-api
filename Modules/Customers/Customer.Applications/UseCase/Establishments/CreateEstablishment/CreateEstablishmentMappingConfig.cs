namespace Customers.Application.UseCase.Establishments.CreateEstablishment;

public class CreateEstablishmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateEstablishmentCommand, Establishment>()
            .ConstructUsing(src => Establishment.Create(
                src.Establishment.Name,
                src.Establishment.Description,
                src.Establishment.ContactInfo.Adapt<ContactInfo>()));
    }
}
