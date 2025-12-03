namespace serenity.Application.DTOs;

/// <summary>
/// Request for Google authentication login.
/// </summary>
public class GoogleLoginRequest
{
    /// <summary>
    /// The Google ID token obtained from the client-side Google Sign-In.
    /// </summary>
    public string IdToken { get; set; } = string.Empty;
}

