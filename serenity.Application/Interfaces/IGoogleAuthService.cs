using Google.Apis.Auth;

namespace serenity.Application.Interfaces;

/// <summary>
/// Service for validating Google ID tokens.
/// </summary>
public interface IGoogleAuthService
{
    /// <summary>
    /// Validates a Google ID token and returns the payload.
    /// </summary>
    /// <param name="idToken">The Google ID token to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Google token payload with user information.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the token is invalid or expired.</exception>
    Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default);
}

