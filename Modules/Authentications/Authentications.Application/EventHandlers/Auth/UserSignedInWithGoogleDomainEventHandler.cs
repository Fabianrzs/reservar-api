using Microsoft.Extensions.Logging;

namespace Authentications.Application.EventHandlers.Auth;

/// <summary>
/// Handles post-login operations when a user signs in with Google.
/// </summary>
public class UserSignedInWithGoogleDomainEventHandler(
    ILogger<UserSignedInWithGoogleDomainEventHandler> logger
) : INotificationHandler<UserSignedInWithGoogleDomainEvent>
{
    public Task Handle(UserSignedInWithGoogleDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User signed in with Google: {UserId} - {Email} at {Time}",
            notification.UserId, notification.Email, notification.OccurredOnUtc);

        return Task.CompletedTask;
    }
}
