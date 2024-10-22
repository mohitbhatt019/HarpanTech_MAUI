using HarpenTech.ViewModels;

namespace HarpenTech.Views.RepairPage;

public partial class RepairView : ContentPage
{
    /// <summary>
    /// Constructor for the RepairView
    /// </summary>
    /// <param name="repairViewModel">The ViewModel for data binding</param>
    public RepairView(RepairViewModel repairViewModel)
    {
        // Initialize the view components
        InitializeComponent();

        // Set the ViewModel for data binding
        BindingContext = repairViewModel;
    }

    /// <summary>
    /// To Execute when focus on UserName textfield
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserNameLabelFocus(object sender, FocusEventArgs e)
    {
        UserLabel.IsVisible = true;
    }

    /// <summary>
    /// To Execute when Unfocus on UserName textfield
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserNameLabelUnFocus(object sender, FocusEventArgs e)
    {
        UserLabel.IsVisible = false;
    }
}
