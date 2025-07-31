namespace Authentications.Domain.Entities;

public class Permission : Entity
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    private Permission() { }

    private Permission(string code, string description)
    {
        Id = Guid.NewGuid();
        Code = code;
        Description = description;
    }

    public static Permission Create(string code, string description) => new(code, description);
}
