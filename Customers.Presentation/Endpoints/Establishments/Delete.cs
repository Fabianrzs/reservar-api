using Customers.Application.UseCase.Establishments.DeleteEstablishment;

namespace Customers.Presentation.Endpoints.Establishments;

public sealed class DeleteEstablishmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/establishments/{id:guid}", async (
            Guid id,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            DeleteEstablishmentCommand command = new(id);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(
                () => Results.NoContent(),
                CustomResult.Problem);
        })
        .WithTags(Tags.Establishments)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
