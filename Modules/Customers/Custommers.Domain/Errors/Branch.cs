namespace Custommers.Domain.Errors;

public static class Branch
{
    public static readonly Error NotFound = Error.NotFound(
        code: "Customer.Branch.NotFound",
        description: "The requested branch does not exist."
    );

    public static readonly Error AlreadyExists = Error.Conflict(
        code: "Customer.Branch.AlreadyExists",
        description: "A branch with the same identifier already exists."
    );

    public static readonly Error Inactive = Error.Conflict(
        code: "Customer.Branch.Inactive",
        description: "The branch is inactive."
    );
}
