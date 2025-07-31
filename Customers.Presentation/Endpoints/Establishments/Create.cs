using Customers.Application.UseCase.Establishments;
using Customers.Application.UseCase.Establishments.CreateEstablishment;

namespace Customers.Presentation.Endpoints.Establishments;

public sealed class CreateEstablishmentEndpoint : IEndpoint
{
    public sealed class Request
    {
        public EstablishmentDto Establishment { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/establishments", async (
            [FromBody] Request request,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            CreateEstablishmentCommand command = 
                request.Adapt<CreateEstablishmentCommand>();

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created,
                CustomResult.Problem);
        })
        .WithTags(Tags.Establishments)
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
