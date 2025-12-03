namespace serenity.Infrastructure.Configuration;

/// <summary>
/// Configuration settings for Google Authentication.
/// </summary>
public class GoogleAuthSettings
{
    /// <summary>
    /// Google OAuth 2.0 Client ID.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Google OAuth 2.0 Client Secret.
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;
}

