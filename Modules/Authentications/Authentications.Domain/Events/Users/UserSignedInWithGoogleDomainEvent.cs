namespace Authentications.Domain.Events.Users;

/// <summary>
/// Raised when a user signs in using Google OAuth.
/// </summary>
public record UserSignedInWithGoogleDomainEvent(
    Guid UserId,
    string Email,
    DateTime OccurredOnUtc
) : IDomainEvent
{
    public Guid Id { get; init; } 
}
