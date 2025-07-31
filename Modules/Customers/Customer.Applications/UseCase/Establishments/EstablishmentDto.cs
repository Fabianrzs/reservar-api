using Customers.Application.UseCase.Branches;
using Customers.Application.UseCase.ContactsInfo;

namespace Customers.Application.UseCase.Establishments;

public record EstablishmentDto(
    string Name,
    string Description,
    ContactInfoDto ContactInfo,
    List<BranchDto>? Branches
);
