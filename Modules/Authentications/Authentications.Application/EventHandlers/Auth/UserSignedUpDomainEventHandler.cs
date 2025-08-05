using Microsoft.Extensions.Logging;

namespace Authentications.Application.EventHandlers.Auth;

/// <summary>
/// Handles post-login operations when a user signs in with Google.
/// </summary>
public class UserSignedUpDomainEventHandler(
    ILogger<UserSignedUpDomainEventHandler> logger
) : INotificationHandler<UserSignedUpDomainEvent>
{
    public Task Handle(UserSignedUpDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User signed Up with Google: {UserId} at {Time}",
            notification.Id, notification.OccurredOnUtc);

        return Task.CompletedTask;
    }
}
