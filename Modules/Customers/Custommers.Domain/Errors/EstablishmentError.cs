namespace Customers.Domain.Errors;

public static class EstablishmentError
{
    public static readonly Error NotFound = Error.NotFound(
        code: "Customer.Establishment.NotFound",
        description: "The requested establishment does not exist."
    );

    public static readonly Error AlreadyExists = Error.Conflict(
        code: "Customer.Establishment.AlreadyExists",
        description: "An establishment with the same identifier already exists."
    );

    public static readonly Error Inactive = Error.Conflict(
        code: "Customer.Establishment.Inactive",
        description: "The establishment is inactive."
    );
}
