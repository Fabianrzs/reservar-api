namespace Customers.Application.UseCase.Establishments.DeleteEstablishment;

public sealed record DeleteEstablishmentCommand(Guid Id) : ICommand;

