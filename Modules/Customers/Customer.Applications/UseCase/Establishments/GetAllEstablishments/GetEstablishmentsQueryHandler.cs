namespace Customers.Application.UseCase.Establishments.GetAllEstablishments;

public sealed class GetEstablishmentsQueryHandler(
    IEstablishmentRepository repository
) : IQueryHandler<GetEstablishmentsQuery, PagedResult<EstablishmentDto>>
{
    public async Task<Result<PagedResult<EstablishmentDto>>> Handle(
        GetEstablishmentsQuery request,
        CancellationToken cancellationToken)
    {
        PagedResult<Establishment> result = await repository.PaginateAsync(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            predicate: e => e.IsActive,
            includes: [e => e.Branches, e => e.ContactInfo],
            orderBy: q => q.OrderBy(e => e.Name),
            cancellationToken: cancellationToken
        );

        return new PagedResult<EstablishmentDto>(
            result.Items.Adapt<List<EstablishmentDto>>(),
            result.TotalCount,
            result.PageNumber,
            result.PageSize
        );
    }
}
