namespace Authentications.Application.Abstractions.Services.Auth;
/// <summary>
/// Define un contrato para la generación de tokens de acceso y actualización.
/// </summary>
public interface ITokenProvider
{ 
    /// <summary>
    /// Genera un token de acceso a partir de un diccionario de pares clave-valor.
    /// </summary>
    /// <param name="claimsPairs"></param>
    /// <returns></returns>
    string GenerateAccessToken(Guid sessionId, User user);

    /// <summary>
    /// Genera un token de acceso a partir de un diccionario de pares clave-valor y una fecha de expiración.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    string GenerateRefreshToken(Guid sessionId, Guid userId);
}
