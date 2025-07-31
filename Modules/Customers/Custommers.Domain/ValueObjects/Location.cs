namespace Customers.Domain.ValueObjects;

public class Location
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Location() { }

    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location Create(double latitude, double longitude)
        => new(latitude, longitude);
}
