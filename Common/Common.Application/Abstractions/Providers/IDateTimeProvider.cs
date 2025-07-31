namespace Common.Application.Abstractions.Providers;

/// <summary>
/// Provides the current system time.
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
}
