namespace Customers.Application.UseCase.ContactsInfo;

public record ContactInfoDto(
string PhoneNumber,
string? Email,
string? Website,
string? InstagramHandle,
string? FacebookHandle,
string? WhatsappNumber
);
