namespace HarpenTech;

// This class holds global settings and constants used throughout the application.
public class GlobalSetting
{
    // Base URL for the API endpoints.
    public const string ApiBaseUrl = "https://ccms-har.harpan.co.za/";
    public const string ApiBackendUrl = "http://localhost:5279/api/";

    // Default constructor for the GlobalSetting class.
    public GlobalSetting()
    {
        // Additional initialization logic can be added here if needed.
    }

    // Client ID used for authentication.
    public string ClientId { get; } = "maui";

    // Client Secret used for authentication.
    public string ClientSecret { get; } = "secret";

    // Property to store the authentication token.
    public string AuthToken { get; set; }
}
