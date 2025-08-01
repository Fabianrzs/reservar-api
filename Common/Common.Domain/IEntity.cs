namespace Common.Domain;

public interface IEntity<T>
{
    T Id { get; set; }
    bool IsActive { get; set; }
}
