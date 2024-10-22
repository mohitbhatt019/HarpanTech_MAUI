namespace HarpenTech.Services;

// INavigationService interface defines the contract for navigation within the application
public interface INavigationService
{
    // Asynchronously initializes the navigation service
    Task InitializeAsync();

    // Asynchronously navigates to a specified route with optional route parameters
    Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);

    // Asynchronously pops the current page from the navigation stack
    Task PopAsync();
}
