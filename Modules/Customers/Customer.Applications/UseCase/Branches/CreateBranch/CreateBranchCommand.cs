namespace Customers.Application.UseCase.Branches.CreateBranch;

public sealed record CreateBranchCommand(BranchDto Branch) : ICommand<Branch>;
