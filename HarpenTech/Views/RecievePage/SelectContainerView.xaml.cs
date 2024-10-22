using HarpenTech.Services;
using HarpenTech.ViewModels;

namespace HarpenTech.Views.RecievePage;

public partial class SelectContainerView : ContentPage
{
    private readonly InspectContainerViewModel _inspectContainerViewModel;
    private INavigationService _navigationService;
    private ViewCell _lastCell;

    public SelectContainerView(InspectContainerViewModel inspectContainerViewModel, INavigationService navigationService)
    {
        InitializeComponent();
        myListView.ItemsSource = vehicles;
        _inspectContainerViewModel = inspectContainerViewModel;
        _navigationService = navigationService;

    }

    //Static list for Vehicles
    List<string> vehicles = new List<string>
    {
        "  Container no.",
        "  Container no.",
        "  Container no.",
        "  Container no.",
    };

    /// <summary>
    /// Event handler for item selection in the current view
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new InspectContainerView(_inspectContainerViewModel, _navigationService));
    }

    /// <summary>
    /// Event handler for item selection to change the color
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ViewCell_Tapped(object sender, EventArgs e)
    {

        if (_lastCell != null)
            _lastCell.View.BackgroundColor = Color.FromRgb(0.50, 0.50, 0.50);
        var viewCell = (ViewCell)sender;
        if (viewCell.View != null)
        {
            viewCell.View.BackgroundColor = Color.FromRgb(0.50, 0.50, 0.50);
            _lastCell = viewCell;
            await Shell.Current.Navigation.PushAsync(new InspectContainerView(_inspectContainerViewModel, _navigationService));
        }
        else
            await Shell.Current.Navigation.PushAsync(new InspectContainerView(_inspectContainerViewModel, _navigationService));
    }
}