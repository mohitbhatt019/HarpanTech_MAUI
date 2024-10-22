namespace HarpenTech.Services.Settings;

// Interface defining application settings related to authentication and mock usage.
public interface ISettingsService
{
    /// <summary>
    /// Gets or sets the authentication access token.
    /// </summary>
    string AuthAccessToken { get; set; }

    /// <summary>
    /// Gets or sets the authentication ID token.
    /// </summary>
    string AuthIdToken { get; set; }

    /// <summary>
    /// Gets or sets the authentication client ID.
    /// </summary>
    string AuthClient_id { get; set; }

    /// <summary>
    /// Gets or sets the authentication grant type.
    /// </summary>
    string AuthGrant_type { get; set; }

    /// <summary>
    /// Gets or sets the authentication offline access.
    /// </summary>
    string AuthOffline_access { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating whether to use mock services.
    /// </summary>
    bool UseMocks { get; set; }
}
