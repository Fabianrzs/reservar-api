namespace Customers.Application.UseCase.Establishments.UpdateEstablishment;

public sealed record UpdateEstablishmentCommand(
    Guid Id,
    EstablishmentDto Establishment
) : ICommand;
