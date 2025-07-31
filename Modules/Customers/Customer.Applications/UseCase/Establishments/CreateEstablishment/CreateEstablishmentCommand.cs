namespace Customers.Application.UseCase.Establishments.CreateEstablishment;

public sealed record CreateEstablishmentCommand(
    EstablishmentDto Establishment
) : ICommand<Guid>;
