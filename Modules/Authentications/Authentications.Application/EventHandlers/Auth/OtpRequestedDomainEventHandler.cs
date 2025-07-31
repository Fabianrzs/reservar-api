using Authentications.Domain.Events.OTP;
using Microsoft.Extensions.Logging;

namespace Authentications.Application.EventHandlers.Auth;

/// <summary>
/// Handles OTP token creation events, useful for sending email or SMS notifications.
/// </summary>
public class OtpRequestedDomainEventHandler(
    ILogger<OtpRequestedDomainEventHandler> logger
) : INotificationHandler<OtpRequestedDomainEvent>
{
    public Task Handle(OtpRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("OTP generated for Id: {Id}, OccurredOnUtc: {OccurredOnUtc}",
            notification.Id, notification.OccurredOnUtc);

        return Task.CompletedTask;
    }
}
