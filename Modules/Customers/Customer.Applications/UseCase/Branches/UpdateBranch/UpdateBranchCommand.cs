namespace Customers.Application.UseCase.Branches.UpdateBranch;

public sealed record UpdateBranchCommand(
    Guid Id,
    BranchDto Branch
) : ICommand;
