using System.Security.Claims;

namespace Authentications.Infrastructure.Extensions;

public static class ClaimHelper
{
    public static List<Claim> BuildClaims(Dictionary<string, object> claimsPairs)
    {
        var claims = new List<Claim>();
        foreach (KeyValuePair<string, object> pair in claimsPairs)
        {
            if (pair.Value is List<string> values && pair.Key == ClaimTypes.Role)
            {
                claims.AddRange(values.Select(value => new Claim(pair.Key, value)));
            }
            else
            {
                claims.Add(new Claim(pair.Key, pair.Value.ToString() ?? string.Empty));
            }

        }
        return claims;
    }

}
