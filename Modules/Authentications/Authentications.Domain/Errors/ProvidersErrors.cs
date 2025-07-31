namespace Authentications.Domain.Errors;

/// <summary>
/// Errors related to federated identity providers (Google, Entra ID, etc.).
/// </summary>
public static class ProvidersErrors
{
    public static readonly Error InvalidExternalToken = Error.Unauthorized(
        code: "Providers.InvalidExternalToken",
        description: "The token provided by the identity provider is invalid."
    );

    public static readonly Error ExternalLoginFailed = Error.Unauthorized(
        code: "Providers.ExternalLoginFailed",
        description: "External login failed."
    );

    public static readonly Error IdentityProviderNotSupported = Error.Validation(
        code: "Providers.IdentityProviderNotSupported",
        description: "This identity provider is not supported."
    );
}
