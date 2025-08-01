namespace Common.Domain;

public abstract class Entity : IEntity<Guid>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; set; }

    public bool IsActive { get; set; } = true;

    protected Entity()
    {
        Id = Guid.NewGuid(); // Puedes quitar esto si prefieres asignar el Id externamente.
    }

    /// <summary>
    /// Marca la entidad como inactiva (soft delete).
    /// </summary>
    public void Inactivate() => IsActive = false;

    /// <summary>
    /// Marca la entidad como activa.
    /// </summary>
    public void Activate() => IsActive = true;

    /// <summary>
    /// Obtiene los eventos de dominio registrados.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Limpia todos los eventos de dominio registrados.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Registra un nuevo evento de dominio.
    /// </summary>
    /// <param name="domainEvent">El evento a registrar.</param>
    public void Raise(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
    }
}
