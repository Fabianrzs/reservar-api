namespace Custommers.Domain.Errors;

public static class User
{
    public static readonly Error NotFound = Error.NotFound(
        code: "Customer.User.NotFound",
        description: "The user associated with this operation was not found."
    );

    public static readonly Error AlreadyAssigned = Error.Conflict(
        code: "Customer.User.AlreadyAssigned",
        description: "The user is already assigned to this establishment."
    );

    public static readonly Error NotAssigned = Error.NotFound(
        code: "Customer.User.NotAssigned",
        description: "The user is not assigned to this establishment."
    );
}
