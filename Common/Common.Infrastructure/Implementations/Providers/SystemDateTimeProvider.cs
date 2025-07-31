using Common.Application.Abstractions.Providers;

namespace Common.Infrastructure.Implementations.Providers;

/// <summary>
/// Default implementation using system clock.
/// </summary>
public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}
