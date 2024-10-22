using static System.Formats.Asn1.AsnWriter;

namespace HarpenTech.Services.Settings;

// This class implements the ISettingsService interface and is responsible for managing application settings.
public class SettingsService : ISettingsService
{
    #region Setting Constants

    // Constants for accessing and storing access token, ID token, and mock usage in application preferences.
    private const string AccessToken = "access_token";
    private const string IdToken = "id_token";
    private const string IdUseMocks = "use_mocks";

    // Constants for default values and configuration related to authentication.
    private const string Client_id = "";
    private const string Grant_type = "";
    private const string Scope = "";

    // Default values for access token, ID token, and mock usage.
    private readonly string AccessTokenDefault = string.Empty;
    private readonly string IdTokenDefault = string.Empty;
    private readonly bool UseMocksDefault = true;

    // Default values and configuration related to authentication.
    private readonly string AccessClient_id = "Harpan.Web";
    private readonly string AccessGrant_type = "password";
    private readonly string AccessScope = "offline_access";

    #endregion

    #region Settings Properties

    // Property to get and set the authentication access token in application preferences.
    public string AuthAccessToken
    {
        get => Preferences.Get(AccessToken, AccessTokenDefault);
        set => Preferences.Set(AccessToken, value);
    }

    // Property to get and set the authentication ID token in application preferences.
    public string AuthIdToken
    {
        get => Preferences.Get(IdToken, IdTokenDefault);
        set => Preferences.Set(IdToken, value);
    }

    // Property to get and set the usage of mock data in application preferences.
    public bool UseMocks
    {
        get => Preferences.Get(IdUseMocks, UseMocksDefault);
        set => Preferences.Set(IdUseMocks, value);
    }

    // Property to get and set the authentication client ID in application preferences.
    public string AuthClient_id
    {
        get => Preferences.Get(Client_id, AccessClient_id);
        set => Preferences.Set(Client_id, value);
    }

    // Property to get and set the authentication grant type in application preferences.
    public string AuthGrant_type
    {
        get => Preferences.Get(Grant_type, AccessGrant_type);
        set => Preferences.Set(Grant_type, value);
    }

    // Property to get and set the authentication scope (offline access) in application preferences.
    public string AuthOffline_access
    {
        get => Preferences.Get(Scope, AccessScope);
        set => Preferences.Set(Scope, value);
    }

    #endregion
}
