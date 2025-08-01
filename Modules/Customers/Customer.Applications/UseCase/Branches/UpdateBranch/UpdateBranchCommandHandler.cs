namespace Customers.Application.UseCase.Branches.UpdateBranch;

public sealed class UpdateBranchCommandHandler(
    IBranchRepository branchRepository
) : ICommandHandler<UpdateBranchCommand>
{
    public async Task<Result> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        Branch? branch = await branchRepository.GetByIdAsync(request.Id, cancellationToken : cancellationToken);
        if (branch is null)
        {
            return Result.Failure(BranchError.NotFound);
        }

        if (!string.IsNullOrWhiteSpace(request.Branch.Name) && request.Branch.Name != branch.Name)
        {
            branch.UpdateName(request.Branch.Name);
        }

        if (request.Branch.Address is not null)
        {
            branch.UpdateAddress(request.Branch.Address.Adapt<Address>());
        }

        if (request.Branch.Location is not null)
        {
            branch.UpdateLocation(request.Branch.Location.Adapt<Location>());
        }

        await branchRepository.UpdateAsync(branch, cancellationToken);

        return Result.Success();
    }
}
