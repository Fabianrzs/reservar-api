using Customers.Application.UseCase.Branches;
using Customers.Application.UseCase.Branches.CreateBranch;
using Customers.Domain.Entities;

namespace Customers.Presentation.Endpoints.Branches;

public sealed class CreateBranchEndpoint : IEndpoint
{
    public sealed class Request
    {
        public BranchDto Branch { get; set; } = default!;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/branches", async (
            Request request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var command = new CreateBranchCommand(request.Branch);
            Result<Branch> result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Created,
                 CustomResult.Problem);
        })
        .WithTags(Tags.Branchs)
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
