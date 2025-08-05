using Authentications.Application.UseCase.Authentications;
using Authentications.Application.UseCase.Authentications.Auth.SignIn;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class SignIn : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"auth/signin", async (
            [FromBody] SignInCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<AuthenticationDto> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResult.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Auth);
    }
}
