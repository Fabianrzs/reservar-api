namespace Customers.Application.UseCase.Branches.DeleteBranch;

public sealed record DeleteBranchCommand(
    Guid Id
) : ICommand;
