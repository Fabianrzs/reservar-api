using Authentications.Application.UseCase.Authentications;
using Authentications.Application.UseCase.Authentications.Auth.Otp.Request;
using Authentications.Application.UseCase.Authentications.Auth.Otp.Validate;

namespace Authentications.Presentation.Endpoints.Auth;

public sealed class Otp : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Tags.Auth}/otp/request", async (
            [FromBody] RequestOtpCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResult.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Auth);

        app.MapPost($"{Tags.Auth}/otp/validate", async (
            [FromBody] ValidateOtpCommand command,
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
