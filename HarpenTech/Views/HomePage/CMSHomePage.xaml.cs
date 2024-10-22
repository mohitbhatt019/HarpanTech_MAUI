namespace HarpenTech.Views.HomePage;

using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;

/// <summary>
/// Partial class representing the content page for the CMS home
/// </summary>
public partial class CMSHomePage : ContentPage
{
    /// Private field to store the associated HomeViewModel
    private readonly HomeViewModel _homeViewModel;

    // Constructor for CMSHomePage, taking a HomeViewModel and ISecureStorageService as parameters
    public CMSHomePage(HomeViewModel homeViewModel, ISecureStorageService storageService)
    {
        // Initialize the private field with the provided view model
        _homeViewModel = homeViewModel;

        // Set the binding context to the provided view model
        BindingContext = homeViewModel;

        // Initialize the components of the page
        InitializeComponent();
    }

    /// <summary>
    /// This method is called when the Page is about to appear on the screen.
    /// </summary>
    protected override void OnAppearing()
    {
        var stack = Application.Current.MainPage.Navigation.NavigationStack.ToArray();

        for (int i = stack.Length - 1; i > 0; i--)
        {
            Application.Current.MainPage.Navigation.RemovePage(stack[i]);
        }
        base.OnAppearing();
    }
}
