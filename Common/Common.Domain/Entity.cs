namespace Common.Domain;

public abstract class Entity : IEntity<Guid>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public Guid Id { get; set; }
    protected Entity()
    {
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
