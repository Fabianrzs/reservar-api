using System.Text;
using System.Text.Json;
using System.Web;
using Authentications.Application.UseCase.Authentications;
using Authentications.Application.UseCase.Authentications.Auth.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class Google : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"auth/google/login", async (HttpContext httpContext,
            [FromQuery] string? redirectUri) =>
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = $"api/auth/google/callback"
            };

            properties.Items["redirectUri"] = redirectUri;

            await httpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);

            return Results.Challenge(properties, [GoogleDefaults.AuthenticationScheme]);

        })
        .AllowAnonymous()
        .WithTags(Tags.Auth);

        app.MapGet($"auth/google/callback", async (
            HttpContext httpContext,[FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            AuthenticateResult authResult = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            string redirectUri = authResult.Properties!.Items["redirectUri"];

            Result<AuthenticationDto> result = await sender.Send(new GoogleAuthCommand(authResult.Principal!), cancellationToken);

            return result.Match(
                success =>
                {
                    string json = JsonSerializer.Serialize(success);
                    string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
                    string encodedSession = HttpUtility.UrlEncode(base64);

                    return Results.Redirect($"{redirectUri}?session={encodedSession}");
                },
                error =>
                {
                    string encodedError = HttpUtility.UrlEncode("no-auth");
                    return Results.Redirect($"{redirectUri}?error={encodedError}");
                });

        })
        .RequireAuthorization(GoogleDefaults.AuthenticationScheme)
        .WithTags(Tags.Auth);
    }
}
