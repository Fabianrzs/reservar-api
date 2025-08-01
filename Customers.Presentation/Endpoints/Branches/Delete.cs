using Customers.Application.UseCase.Branches.DeleteBranch;

namespace Customers.Presentation.Endpoints.Branches;

public sealed class DeleteBranchEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/branches/{branchId:guid}", async (
            Guid branchId,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var command = new DeleteBranchCommand(branchId);
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(
               () => Results.NoContent(),
               CustomResult.Problem);
        })
        .WithTags(Tags.Branchs)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
