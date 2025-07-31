namespace Customers.Application.UseCase.ContactsInfo;

public class ContactInfoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ContactInfoDto, ContactInfo>()
            .ConstructUsing(dto => ContactInfo.Create(
                dto.PhoneNumber,
                dto.Email,
                dto.Website,
                dto.InstagramHandle,
                dto.FacebookHandle,
                dto.WhatsappNumber
            ));
    }
}
