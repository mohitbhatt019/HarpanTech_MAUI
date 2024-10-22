using CommunityToolkit.Mvvm.Input;
using HarpenTech.Services;

namespace HarpenTech.ViewModels.Base;

// Interface representing the base structure for view models in the MAUI application
public interface IViewModelBase : IQueryAttributable
{
    // Property to access the navigation service for navigation within the application
    public INavigationService NavigationService { get; }

    // Asynchronous relay command for initializing the view model asynchronously
    public IAsyncRelayCommand InitializeAsyncCommand { get; }

    // Property indicating whether the view model is currently busy with an operation
    public bool IsBusy { get; }

    // Property indicating whether the view model has been initialized
    public bool IsInitialized { get; }

    // Method for asynchronous initialization of the view model
    Task InitializeAsync();
}
