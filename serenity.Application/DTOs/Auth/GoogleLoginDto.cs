namespace serenity.Application.DTOs.Auth;

/// <summary>
/// DTO for Google authentication login request.
/// </summary>
public class GoogleLoginDto
{
    /// <summary>
    /// The Google ID token obtained from the client-side Google Sign-In.
    /// </summary>
    public string IdToken { get; set; } = string.Empty;
}

