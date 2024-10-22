using HarpenTech.Services.Settings;

namespace HarpenTech.Services;

// MauiNavigationService class implements the INavigationService interface for navigation within the MAUI application
public class MauiNavigationService : INavigationService
{
    // Service for accessing and managing application settings
    private readonly ISettingsService _settingsService;

    // Constructor for the MauiNavigationService class
    public MauiNavigationService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    // Asynchronously initializes the navigation service by navigating to the Login or Main/Catalog page based on the authentication token
    public Task InitializeAsync() =>
        NavigateToAsync(
            string.IsNullOrEmpty(_settingsService.AuthAccessToken)
                ? "//Login"
                : "//Main/Catalog");

    // Asynchronously navigates to a specified route with optional route parameters
    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
    {
        // Create a new ShellNavigationState for the specified route
        var shellNavigation = new ShellNavigationState(route);

        // Navigate to the specified route with or without route parameters
        return routeParameters != null
            ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
            : Shell.Current.GoToAsync(shellNavigation);
    }

    // Asynchronously pops the current page from the navigation stack
    public Task PopAsync() =>
        Shell.Current.GoToAsync("..");
}
