using Customers.Domain.ValueObjects;

namespace Customers.Domain.Entities;

public class Establishment : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Uri? PhotoUrl { get; private set; }
    public Uri? BannerUrl { get; private set; }

    public ContactInfo ContactInfo { get; private set; }

    private readonly List<EstablishmentUser> _users = [];
    public IReadOnlyCollection<EstablishmentUser> Users => _users.AsReadOnly();

    private readonly List<Branch> _branches = [];
    public IReadOnlyCollection<Branch> Branches => _branches.AsReadOnly();

    public Establishment() { }

    public Establishment(string name, string description, ContactInfo contactInfo)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required", nameof(description));
        }

        ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));

        Name = name;
        Description = description;
    }

    public static Establishment Create(string name, string description, ContactInfo contactInfo) =>
        new(name, description, contactInfo);

    public void AddUser(EstablishmentUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (_users.Any(u => u.UserId == user.UserId))
        {
            throw new InvalidOperationException("User already exists in this establishment.");
        }

        _users.Add(user);
    }

    public void AddBranch(Branch branch)
    {
        ArgumentNullException.ThrowIfNull(branch);
        if (_branches.Any(b => b.Name == branch.Name))
        {
            throw new InvalidOperationException("A branch with the same name already exists.");
        }

        _branches.Add(branch);
    }

    public void UpdateContactInfo(ContactInfo contactInfo)
    {
        ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));
    }

    public void UpdatePhoto(Uri photoUrl)
    {
        PhotoUrl = photoUrl;
    }

    public void UpdateBanner(Uri bannerUrl)
    {
        BannerUrl = bannerUrl;
    }

    public void UpdateDetails(string name, string description, ContactInfo contactInfo)
    {
        Name = name;
        Description = description;
        ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));
    }
}
