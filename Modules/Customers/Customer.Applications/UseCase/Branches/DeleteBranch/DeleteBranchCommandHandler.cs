namespace Customers.Application.UseCase.Branches.DeleteBranch;

public sealed class DeleteBranchCommandHandler(
    IBranchRepository branchRepository
) : ICommandHandler<DeleteBranchCommand>
{
    public async Task<Result> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        Branch? brach = await branchRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (brach is null)
        {
            return Result.Failure(BranchError.NotFound);
        }

        await branchRepository.DeleteAsync(brach.Id, cancellationToken);

        return Result.Success();
    }
}
