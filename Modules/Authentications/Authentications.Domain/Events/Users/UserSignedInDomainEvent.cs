
namespace Authentications.Domain.Events.Users;

public record UserSignedInDomainEvent(Guid Id, DateTime OccurredOnUtc) 
    : IDomainEvent;
