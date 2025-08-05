namespace Authentications.Domain.Events.Users;

/// <summary>
/// Raised when a user signs in using Google OAuth.
/// </summary>
public record UserSignedUpDomainEvent(Guid Id, DateTime OccurredOnUtc) : IDomainEvent;
