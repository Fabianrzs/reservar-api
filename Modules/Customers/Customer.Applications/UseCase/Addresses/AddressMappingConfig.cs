namespace Customers.Application.UseCase.Addresses;

public class AddressMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddressDto, Address>()
            .ConstructUsing(dto => Address.Create(
                dto.Street,
                dto.Number,
                dto.Neighborhood,
                dto.City,
                dto.State,
                dto.Country,
                dto.ZipCode
            ));
    }
}
