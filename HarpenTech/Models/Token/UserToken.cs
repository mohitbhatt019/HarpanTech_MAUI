using System.Text.Json.Serialization;

namespace HarpenTech.Models.Token;

// UserToken class represents a model for user authentication tokens
public class UserToken
{
    // Gets or sets the identity token associated with the user
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }

    // Gets or sets the access token received from the authorization server
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    // Gets or sets the duration in seconds for which the access token is valid
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    // Gets or sets the type of the token (e.g., "Bearer")
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    // Gets or sets the refresh token used to obtain a new access token
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}
