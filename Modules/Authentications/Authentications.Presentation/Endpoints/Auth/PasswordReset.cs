using Authentications.Application.UseCase.Authentications;
using Authentications.Application.UseCase.Authentications.PasswordReset.Change;
using Authentications.Application.UseCase.Authentications.PasswordReset.Request;
using Authentications.Application.UseCase.Authentications.PasswordReset.Validate;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class PasswordResetEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"auth/password/request", async (
            [FromBody] PasswordResetRequestCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Created, CustomResult.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Auth);

        app.MapPost($"auth/password/validate", async (
            [FromBody] ValidatePasswordResetTokenCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<string> result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Ok, CustomResult.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Auth);

        app.MapPost($"auth/password/change", async (
            [FromBody] ChangePasswordCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<AuthenticationDto> result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Created, CustomResult.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Auth);
    }
}
