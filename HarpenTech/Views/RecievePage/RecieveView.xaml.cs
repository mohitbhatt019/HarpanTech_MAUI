using CommunityToolkit.Maui.Core.Platform;
using HarpenTech.Models.Container;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using UraniumUI.Material.Controls;

namespace HarpenTech.Views.RecievePage;

public partial class RecieveView : ContentPageBase
{
    private readonly DatabaseContext _context;
    private readonly IRequestProvider _requestProvider;
    private readonly InspectContainerViewModel _inspectViewModel;
    private readonly RecieveViewModel _viewModel;


    // Service for secure storage (assuming this is dependency injected)
    private ISecureStorageService _storageService;

    // Navigation service for later use (assuming this is dependency injected)
    INavigationService navigationService;

    // Constructor for the view
    public RecieveView(ISecureStorageService storageService, DatabaseContext context, IRequestProvider requestProvider, RecieveViewModel recieveViewModel, InspectContainerViewModel inspectContainerViewModel)
    {

        BindingContext = recieveViewModel;
        _storageService = storageService;
        _requestProvider = requestProvider;
        _context = context;
        _viewModel = recieveViewModel;
        _inspectViewModel = inspectContainerViewModel;
        // Set the ViewModel for data binding
        // Initialize the secure storage service
        _storageService = storageService;
        // Initialize the view components
        InitializeComponent();
    }






    /// <summary>
    /// Event handler for the "Edit" button click
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="args">The event arguments</param>

    private async void EditClick(object sender, SelectedItemChangedEventArgs args)
    {
        // Extract ContainerInfo from the selected item
        ContainerInfo containerItem = (args.SelectedItem as ContainerInfo);
        // Navigate to the Edit view with the selected ContainerInfo
        await Navigation.PushAsync(new NavigationPage(new RecieveEditView(_requestProvider, containerItem, _context, _storageService)));
    }

    /// <summary>
    /// Event handler for item selection
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // Check if the selected item is of type ContainerInfo
        if (e.SelectedItem is ContainerInfo selectedContainer)
        {
            ContainerInfo containerInfo = (e.SelectedItem as ContainerInfo);
            // Retrieve ViewModel from the binding context
            var viewModel = (RecieveViewModel)BindingContext;

            // Navigate to the Edit view with the selected ContainerInfo
            await Shell.Current.Navigation.PushAsync(new RecieveEditView(_requestProvider, containerInfo, _context, _storageService));
        }
    }

    /// <summary>
    /// To Execute on SearchClick
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>

    private async void SearchClick(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Search", "No Results Found", "Ok");
    }

    /// <summary>
    /// To Execute on BackButton Click
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>

    private async void BackClick(object sender, EventArgs e)
    {
        await ((RecieveViewModel)BindingContext).BackClickCommand.ExecuteAsync(e);
    }

    /// <summary>
    /// Event to clear the text from the searched field
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ClearSearch(object sender, EventArgs e)
    {
        SearchBarText.Text = string.Empty;
    }

    /// <summary>
    /// Event to handle search button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void SearchClickButton(object sender, EventArgs e)
    {
        _viewModel.SearchClickRespone();
        /*await Shell.Current.Navigation.PushAsync(new SelectVehicleView(_inspectViewModel, navigationService));*/
    }

}
