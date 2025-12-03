using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Configuration;

namespace serenity.Infrastructure.Adapters.Services;

/// <summary>
/// Google authentication service implementation using Google.Apis.Auth.
/// </summary>
public class GoogleAuthService : IGoogleAuthService
{
    private readonly GoogleAuthSettings _settings;

    public GoogleAuthService(IOptions<GoogleAuthSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(idToken))
        {
            throw new ArgumentException("ID token is required.", nameof(idToken));
        }

        if (string.IsNullOrWhiteSpace(_settings.ClientId))
        {
            throw new InvalidOperationException("Google ClientId is not configured.");
        }

        try
        {
            // Validar el token de Google con el ClientId
            var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _settings.ClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

            if (payload == null)
            {
                throw new InvalidOperationException("Invalid Google ID token.");
            }

            return payload;
        }
        catch (InvalidJwtException ex)
        {
            throw new InvalidOperationException($"Invalid Google ID token: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error validating Google token: {ex.Message}", ex);
        }
    }
}

