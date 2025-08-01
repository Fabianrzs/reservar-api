using Customers.Application.UseCase.Addresses;
using Customers.Application.UseCase.Locations;

namespace Customers.Application.UseCase.Branches;
public record BranchDto(
    Guid? Id,
    Guid EstablishmentId,
    string Name,
    AddressDto Address,
    LocationDto Location
);
