using Customers.Application.UseCase.Establishments;
using Customers.Application.UseCase.Establishments.GetAllEstablishments;

namespace Customers.Presentation.Endpoints.Establishments;

public sealed class GetAllEstablishmentsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/establishments", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetEstablishmentsQuery(pageNumber, pageSize);
            Result<PagedResult<EstablishmentDto>> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok,CustomResult.Problem);
        })
        .WithTags(Tags.Establishments)
        .Produces<PagedResult<EstablishmentDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
