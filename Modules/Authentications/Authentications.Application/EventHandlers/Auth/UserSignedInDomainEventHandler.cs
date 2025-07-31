using Microsoft.Extensions.Logging;

namespace Authentications.Application.EventHandlers.Auth;

/// <summary>
/// Handles post-login operations when a user signs.
/// </summary>
public class UserSignedInDomainEventHandler(
    ILogger<UserSignedInDomainEventHandler> logger
) : INotificationHandler<UserSignedInDomainEvent>
{
    public Task Handle(UserSignedInDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User signed: {UserId} at {Time}",
            notification.Id, notification.OccurredOnUtc);

        return Task.CompletedTask;
    }
}
