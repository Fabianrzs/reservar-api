namespace Authentications.Domain.ValueObjects;

/// <summary>
/// Value object that represents a secure hashed password using BCrypt.
/// </summary>
public readonly struct HashedPassword : IEquatable<HashedPassword>
{
    public string Value { get; }

    private HashedPassword(string hashedValue)
    {
        if (string.IsNullOrWhiteSpace(hashedValue))
        {
            throw new ArgumentException("Password hash cannot be empty", nameof(hashedValue));
        }

        Value = hashedValue;
    }

    /// <summary>
    /// Creates a hashed password from a plain text password using BCrypt.
    /// </summary>
    public static HashedPassword Create(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
        {
            throw new ArgumentException("Password cannot be null, empty, or whitespace.", nameof(plainPassword));
        }

        return new HashedPassword(plainPassword);
    }

    /// <summary>
    /// Verifies if the plain password matches the stored hashed password.
    /// </summary>
    public bool Verify(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
        {
            return false;
        }
        return BCrypt.Net.BCrypt.Verify(plainPassword, Value);
    }

    public static HashedPassword HashPassword(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
        {
            throw new ArgumentException("Password cannot be null, empty, or whitespace.", nameof(plainPassword));
        }

        string hashed = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        return new HashedPassword(hashed);
    }

    public override string ToString() => Value;

    public bool Equals(HashedPassword other) => Value == other.Value;

    public override bool Equals(object? obj) => obj is HashedPassword other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(HashedPassword left, HashedPassword right) => left.Equals(right);

    public static bool operator !=(HashedPassword left, HashedPassword right) => !(left == right);
}
