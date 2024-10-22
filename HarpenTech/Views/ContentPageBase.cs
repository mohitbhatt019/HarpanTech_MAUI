using HarpenTech.ViewModels.Base;

namespace HarpenTech.Views;

public abstract class ContentPageBase : ContentPage
{
    // Constructor for the ContentPageBase
    public ContentPageBase()
    {
        // Remove the back button title from the navigation bar
        NavigationPage.SetBackButtonTitle(this, string.Empty);
    }

    // Override of the OnAppearing method
    protected override async void OnAppearing()
    {
        // Call the base class OnAppearing method
        base.OnAppearing();

        // Check if the BindingContext implements IViewModelBase
        if (BindingContext is not IViewModelBase ivmb)
        {
            // Exit if the BindingContext does not implement IViewModelBase
            return;
        }

        // Execute the InitializeAsyncCommand from the ViewModel
        await ivmb.InitializeAsyncCommand.ExecuteAsync(null);
    }
}
