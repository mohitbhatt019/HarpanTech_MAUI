using HarpenTech.NewFolder;
using HarpenTech.ViewModels;

namespace HarpenTech.Views.Dispatch;

/// <summary>
/// Partial class representing the view for dispatch details
/// </summary>
public partial class DispatchView : ContentPage
{
    private readonly DatabaseContext _context;

    /// <summary>
    /// Constructor for DispatchView, taking a DispatchViewModel as a parameter
    /// </summary>
    /// <param name="dispatchViewModel">The view model for dispatch details</param>
    /// <param name="context">The database context</param>
    public DispatchView(DispatchViewModel dispatchViewModel, DatabaseContext context)
    {
        _context = context;
        // Set the binding context to the provided view model
        BindingContext = dispatchViewModel;
        // Initialize the components of the view
        InitializeComponent();


    }

    /// <summary>
    /// Event handler for the DispatchEditClickCommand event
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void DispatchEditClickCommand(object sender, EventArgs e)
    {
        // Execute the DispatchEditClickCommand in the associated view model
        ((DispatchViewModel)BindingContext).DispatchEditClickCommand.Execute(e);
    }
}
