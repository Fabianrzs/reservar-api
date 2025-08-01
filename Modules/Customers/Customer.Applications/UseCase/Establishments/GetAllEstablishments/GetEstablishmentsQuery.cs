namespace Customers.Application.UseCase.Establishments.GetAllEstablishments;

public sealed record GetEstablishmentsQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<EstablishmentDto>>;
