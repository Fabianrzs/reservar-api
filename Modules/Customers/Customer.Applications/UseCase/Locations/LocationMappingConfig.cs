namespace Customers.Application.UseCase.Locations;

public class LocationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LocationDto, Location>()
            .ConstructUsing(dto => Location.Create(
                dto.Latitude,
                dto.Longitude
            ));
    }
}
