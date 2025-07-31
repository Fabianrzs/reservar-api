namespace Common.Application.Abstractions.Providers;

public interface IConnectionStringProvider
{
    string Authentication { get; }
    string Customer { get; }
    string Base { get; }
}
