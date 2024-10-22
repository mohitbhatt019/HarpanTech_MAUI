using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Services;

namespace HarpenTech.ViewModels.Base;

// Partial abstract class representing the base structure for view models in the MAUI application
public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
{
    // Counter for tracking the number of asynchronous operations in progress
    private long _isBusy;

    // Property indicating whether the view model is currently busy with an operation
    public bool IsBusy => Interlocked.Read(ref _isBusy) > 0;

    // Observable property indicating whether the view model has been initialized
    [ObservableProperty]
    private bool _isInitialized;

    // Property to access the navigation service for navigation within the application
    public INavigationService NavigationService { get; }

    // Asynchronous relay command for initializing the view model asynchronously
    public IAsyncRelayCommand InitializeAsyncCommand { get; }

    /// <summary>
    /// Constructor for ViewModelBase, taking INavigationService as a parameter
    /// </summary>
    /// <param name="navigationService">The navigation service used for navigating within the application.</param>
    public ViewModelBase(INavigationService navigationService)
    {
        NavigationService = navigationService;

        // Initialize the asynchronous relay command for initialization
        InitializeAsyncCommand =
            new AsyncRelayCommand(
                async () =>
                {
                    // Set the view model as busy during initialization
                    await IsBusyFor(InitializeAsync);

                    // Mark the view model as initialized
                    IsInitialized = true;
                },
                AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
    }

    /// <summary>
    /// Method for applying query attributes to the view model.
    /// </summary>
    /// <param name="query">A dictionary containing query attributes.</param>
    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Implementation can be provided in derived classes
    }

    /// <summary>
    /// Asynchronous method for initializing the view model.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method for handling asynchronous operations and updating the IsBusy property.
    /// </summary>
    /// <param name="unitOfWork">The asynchronous unit of work to be executed.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>

    public async Task IsBusyFor(Func<Task> unitOfWork)
    {
        // Increment the IsBusy counter and notify property change
        Interlocked.Increment(ref _isBusy);
        OnPropertyChanged(nameof(IsBusy));

        try
        {
            // Execute the asynchronous unit of work
            await unitOfWork();
        }
        finally
        {
            // Decrement the IsBusy counter and notify property change
            Interlocked.Decrement(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));
        }
    }
}

