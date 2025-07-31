namespace Authentications.Domain.Events.OTP;

/// <summary>
/// Raised when an OTP token is generated.
/// </summary>
public record OtpRequestedDomainEvent( Guid Id, DateTime OccurredOnUtc) 
    : IDomainEvent
{
}
