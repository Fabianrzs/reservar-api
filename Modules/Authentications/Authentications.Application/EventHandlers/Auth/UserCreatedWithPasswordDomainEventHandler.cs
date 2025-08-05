using Microsoft.Extensions.Logging;

namespace Authentications.Application.EventHandlers.Auth;

public class UserCreatedWithPasswordDomainEventHandler(
    ILogger<UserCreatedWithPasswordDomainEventHandler> logger
) : INotificationHandler<UserCreatedWithPasswordDomainEvent>
{
    public Task Handle(UserCreatedWithPasswordDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Welcome email with password sent to user {UserId}", notification.Id);
        return Task.CompletedTask;
    }
}
