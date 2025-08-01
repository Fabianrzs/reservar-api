namespace Customers.Application.UseCase.Branches.CreateBranch;

public class CreateBranchCommandHandler : ICommandHandler<CreateBranchCommand, Branch>
{
    public Task<Result<Branch>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        Branch branch = request.Branch.Adapt<Branch>();
        return Task.FromResult(Result.Success(branch));
    }
}
