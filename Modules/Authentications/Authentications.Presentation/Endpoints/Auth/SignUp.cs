using Authentications.Application.UseCase.Authentications.Auth.SignUp;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class SignUp : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"auth/signup", async (
            [FromBody] SignUpCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResult.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Auth);
    }
}
