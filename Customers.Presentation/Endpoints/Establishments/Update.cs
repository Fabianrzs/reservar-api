using Customers.Application.UseCase.Establishments;
using Customers.Application.UseCase.Establishments.UpdateEstablishment;

namespace Customers.Presentation.Endpoints.Establishments;

public sealed class UpdateEstablishmentEndpoint : IEndpoint
{
    public sealed class Request
    {
        public EstablishmentDto Establishment { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/establishments/{id:guid}", async (
            Guid id,
            [FromBody] Request request,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            UpdateEstablishmentCommand command = new(id, request.Establishment);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(
                () => Results.Ok(),
                CustomResult.Problem);
        })
        .WithTags(Tags.Establishments)
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
