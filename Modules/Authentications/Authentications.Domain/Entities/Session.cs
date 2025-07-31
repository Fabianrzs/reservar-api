using Authentications.Domain.Enums;

namespace Authentications.Domain.Entities;

public class Session : Entity
{
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public SessionStatus Status { get; private set; }

    public string Ip { get; private set; }
    public string UserAgent { get; private set; }

    public void Revoke() => Status = SessionStatus.Revoked;
    public void Expire() => Status = SessionStatus.Expired;
    public void Active() => Status = SessionStatus.Active;

    public static Session Create(Guid userId, DateTime createdAt, 
        DateTime expiresAt, string ip, string userAgent)
    {
        return new Session
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = createdAt,
            ExpiresAt = expiresAt,
            Status = SessionStatus.Active,
            Ip = ip,
            UserAgent = userAgent
        };
    }
}
