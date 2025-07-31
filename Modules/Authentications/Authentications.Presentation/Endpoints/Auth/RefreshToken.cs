using Authentications.Application.UseCase.Authentications;
using Authentications.Application.UseCase.Authentications.Auth.RefreshToken;
using Microsoft.AspNetCore.Authorization;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class RefreshToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Tags.Auth}/refresh-token", async (
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RefreshTokenCommand();

            Result<RefreshTokenDto> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResult.Problem);
        })
        .RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "Refresh" })
        .WithTags(Tags.Auth);
    }
}
