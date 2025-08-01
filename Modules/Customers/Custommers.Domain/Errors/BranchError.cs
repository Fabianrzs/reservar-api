namespace Customers.Domain.Errors;

public static class BranchError
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

    public static readonly Error InvalidLocation = Error.Validation(
        code: "Customer.Branch.InvalidLocation",
        description: "The branch location is invalid. Latitude must be between -90 and 90. Longitude must be between -180 and 180."
    );

    public static readonly Error InvalidAddress = Error.Validation(
        code: "Customer.Branch.InvalidAddress",
        description: "The branch must have a valid address including street, number, city and country."
    );
}
