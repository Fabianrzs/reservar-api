using Authentications.Domain.ValueObjects;

namespace Authentications.Domain.Entities;

public class HashedPasswordHistory
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public HashedPassword Password { get; private set; }
    public DateTime SetAt { get; private set; }

    private HashedPasswordHistory() { }

    public static HashedPasswordHistory From(HashedPassword password, DateTime setAt)
        => new() { Password = password, SetAt = setAt };
}
