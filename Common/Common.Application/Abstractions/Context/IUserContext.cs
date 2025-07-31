namespace Common.Application.Abstractions.Context;

public interface IUserContext
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Email { get; set; }
    Guid SessionId { get; set; }
}
