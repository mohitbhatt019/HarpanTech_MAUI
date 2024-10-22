using HarpenTech.Services;
using HarpenTech.ViewModels;
using Microsoft.Maui.Controls.Compatibility;

namespace HarpenTech.Views.RecievePage;

public partial class SelectVehicleView : ContentPage
{
    private InspectContainerViewModel _inspectContainerViewModel;
    private INavigationService _navigationService;
    private ViewCell _lastCell;

    // Constructor for the view
    public SelectVehicleView(InspectContainerViewModel inspectContainerViewModel, INavigationService navigationService)
    {
        InitializeComponent();

        // Set parameters
        _inspectContainerViewModel = inspectContainerViewModel;
        _navigationService = navigationService;

        // Set the ItemSource for the ListView
        myListView.ItemsSource = Status;



    }

    /// <summary>
    /// Custom ViewCell for the ListView
    /// </summary>
    public class MyCell : ViewCell
    {
        /// <summary>
        /// Constructor for MyCell
        /// </summary>
        public MyCell() : base()
        {
            RelativeLayout layout = new RelativeLayout();
            layout.SetBinding(ContentPage.BackgroundColorProperty, new Binding("BackgroundColor"));

            View = layout;
        }
    }

    List<string> Status = new List<string>
    {
        "  Transporter Name",
        "  Transporter Name",
        "  Transporter Name",
        "  Transporter Name"

    };

    /// <summary>
    /// Set colors based on selection
    /// </summary>
    /// <param name="isSelected">Indicates whether the item is selected or not</param>
    public void SetColors(bool isSelected)
    {
        if (isSelected)
        {
            BackgroundColor = Color.FromRgb(0.20, 0.20, 1.0);
        }
        else
        {
            BackgroundColor = Color.FromRgb(0.95, 0.95, 0.95);
        }
    }


    /// <summary>
    /// Event handler for item selection in the current view
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ViewCell selectedViewCell)
        {
            // Set the border color for the selected ViewCell
            // Change to your desired color
            selectedViewCell.View.BackgroundColor = Color.FromRgb(0.80000000, 0.80, 0.80); // You can adjust the border width as needed
            selectedViewCell.View.Background = Color.FromRgb(0.80000000, 0.80, 0.80);

        }

        await Shell.Current.Navigation.PushAsync(new SelectContainerView(_inspectContainerViewModel, _navigationService));
    }



    /// <summary>
    ///  Event handler for item tapped to change the default color
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
            _inspectContainerViewModel.ContainerClickRespone();
        }
        else

            await Shell.Current.Navigation.PushAsync(new SelectContainerView(_inspectContainerViewModel, _navigationService));
    }
    private async void ClearSearch(object sender, EventArgs e)
    {
        SearchEntry.Text = string.Empty;
        await Shell.Current.Navigation.PopAsync();
    }

    private async void SearchClick(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new SelectContainerView(_inspectContainerViewModel, _navigationService));
    }

   
}