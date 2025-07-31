namespace Customers.Domain.ValueObjects;

public class ContactInfo
{
    public string PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string? Website { get; private set; }
    public string? InstagramHandle { get; private set; }
    public string? FacebookHandle { get; private set; }
    public string? WhatsappNumber { get; private set; }

    public ContactInfo() { }
    public ContactInfo(string phoneNumber) {
        PhoneNumber = phoneNumber;
    }

    public ContactInfo(
        string phoneNumber,
        string? email,
        string? website,
        string? instagramHandle,
        string? facebookHandle,
        string? whatsappNumber)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        Website = website;
        InstagramHandle = instagramHandle;
        FacebookHandle = facebookHandle;
        WhatsappNumber = whatsappNumber;
    }

    public static ContactInfo Create(
        string phoneNumber,
        string? email,
        string? website,
        string? instagramHandle,
        string? facebookHandle,
        string? whatsappNumber)
        => new(phoneNumber, email, website, instagramHandle, facebookHandle, whatsappNumber);
    public static ContactInfo Create(string phoneNumber) => new(phoneNumber);
}
