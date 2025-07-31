using Authentications.Application.UseCase.Authentications.Auth.SignOut;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class SignOut : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Tags.Auth}/signout", async (
            [FromBody] SignOutCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResult.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Auth);
    }
}
