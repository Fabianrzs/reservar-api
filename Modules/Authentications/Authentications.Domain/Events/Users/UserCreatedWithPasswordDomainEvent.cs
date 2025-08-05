namespace Authentications.Domain.Events.Users;
public sealed record UserCreatedWithPasswordDomainEvent(
    Guid Id,
    string Email,
    string PlainPassword,
    DateTime OccurredOnUtc
) : IDomainEvent;
