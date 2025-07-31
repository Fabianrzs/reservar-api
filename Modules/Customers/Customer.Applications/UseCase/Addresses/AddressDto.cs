namespace Customers.Application.UseCase.Addresses;
public record AddressDto(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string State,
    string Country,
    string ZipCode
);
