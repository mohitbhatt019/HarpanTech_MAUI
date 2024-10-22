using CommunityToolkit.Mvvm.Input;
using HarpenTech.Services;
using HarpenTech.ViewModels.Base;

namespace HarpenTechApp.ViewModels;

// Partial class for the MainViewModel, extending ViewModelBase
public partial class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Constructor for MainViewModel, taking INavigationService as a parameter
    /// </summary>
    /// <param name="navigationService">Navigation service for navigating within the application.</param>
    public MainViewModel(INavigationService navigationService)
        : base(navigationService)
    {
    }

    /// <summary>
    /// RelayCommand method for handling the settings button click
    /// </summary>
    [RelayCommand]
    private async Task SettingsAsync()
    {
        // Navigate asynchronously to the Settings page
        await NavigationService.NavigateToAsync("Settings");
    }
}
