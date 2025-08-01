using Customers.Application.UseCase.Branches;
using Customers.Application.UseCase.Branches.UpdateBranch;

namespace Customers.Presentation.Endpoints.Branches;

public sealed class UpdateBranchEndpoint : IEndpoint
{
    public sealed class Request
    {
        public Guid BranchId { get; set; }
        public BranchDto Branch { get; set; } = default!;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/branches/{branchId:guid}", async (
            [AsParameters] Request request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var command = new UpdateBranchCommand(request.BranchId, request.Branch);
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(
                () => Results.Ok(),
                CustomResult.Problem);
        })
        .WithTags(Tags.Branchs)
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
