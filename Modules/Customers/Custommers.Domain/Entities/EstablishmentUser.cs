namespace Customers.Domain.Entities;

public class EstablishmentUser : Entity
{
    public Guid EstablishmentId { get; private set; }
    public Establishment Establishment { get; }
    public Guid UserId { get; private set; }

    public EstablishmentUser() { }

    public EstablishmentUser(Guid establishmentId, Guid userId, string role)
    {
        Id = Guid.NewGuid();
        EstablishmentId = establishmentId;
        UserId = userId;
    }

    public static EstablishmentUser Create(Guid establishmentId, Guid userId, string role)
        => new(establishmentId, userId, role);
}
