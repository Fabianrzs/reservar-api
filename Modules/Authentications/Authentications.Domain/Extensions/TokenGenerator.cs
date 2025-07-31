using System.Globalization;
using System.Security.Cryptography;

namespace Authentications.Domain.Extensions;

/// <summary>
/// Provides secure token generation for OTP, password reset, and similar flows.
/// </summary>
public static class TokenGenerator
{
    /// <summary>
    /// Generates a cryptographically secure numeric token with a specified length (default is 6).
    /// </summary>
    /// <param name="length">The length of the numeric token (must be between 1 and 9).</param>
    /// <returns>A numeric token string with leading zeros if needed.</returns>
    public static string GenerateNumericToken(int length = 6)
    {
        if (length < 1 || length > 9)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Token length must be between 1 and 9.");
        }

        int max = (int)Math.Pow(10, length);
        int number = RandomNumberGenerator.GetInt32(0, max);

        return number.ToString($"D{length}", CultureInfo.InvariantCulture);
    }
}
