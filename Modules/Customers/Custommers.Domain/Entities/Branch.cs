using Customers.Domain.ValueObjects;

namespace Customers.Domain.Entities;

public class Branch : Entity
{
    public string Name { get; private set; }

    public Guid EstablishmentId { get; private set; }
    public Establishment Establishment { get; }
    public Address Address { get; private set; }
    public Location Location { get; private set; }

    public Branch() { }

    public Branch(string name, Guid establishmentId, Address address, Location location)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Branch name is required", nameof(name));
        }

        Name = name;
        EstablishmentId = establishmentId;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public static Branch Create(string name, Guid establishmentId, Address address, Location location) =>
        new(name, establishmentId, address, location);

    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
    }

    public void UpdateLocation(Location newLocation)
    {
        Location = newLocation ?? throw new ArgumentNullException(nameof(newLocation));
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
