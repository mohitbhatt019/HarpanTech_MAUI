using HarpenTech.NewFolder;
using HarpenTech.ViewModels;
using HarpenTech.Views.DatabaseScreen;

namespace HarpenTech.Views.Dispatch;

/// <summary>
/// Partial class representing the view for editing dispatch details
/// </summary>
public partial class DispatchEditView : ContentPage
{
    private readonly DatabaseContext _context;

    /// <summary>
    /// Constructor for DispatchEditView, taking a DispatchEditViewModel as a parameter
    /// </summary>
    /// <param name="dispatchEditViewModel">The view model for editing dispatch details</param>
    /// <param name="context">The database context</param>
    public DispatchEditView(DispatchEditViewModel dispatchEditViewModel, DatabaseContext context)
    {
        _context = context;

        // Set the binding context to the provided view model
        BindingContext = dispatchEditViewModel;

        // Initialize the components of the view
        InitializeComponent();
    }

    /// <summary>
    /// To Display saved data on button click
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void DisplayDataClick(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new DispatchViewData(_context));

    }

    /// <summary>
    /// Event handler to save product on button click
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void SaveProduct(object sender, EventArgs e)
    {
        var data = (DispatchEditViewModel)BindingContext;

        ValidateAndSetErrorLabel(data.Customer, CustomerNullErrorLabel, "Customer can not be Null");
        ValidateAndSetErrorLabel(data.BookingReference, BookingReferenceNullErrorLabel, "BookingReference can not be Null");
        ValidateAndSetintErrorLabel(data.ContainerNumber, ContainerNumberNullErrorLabel, "ContainerNumber must be empty");
        ValidateAndSetintErrorLabel(data.Iso, IsoNullErrorLabel, "Iso can not be Null");
        ValidateAndSetErrorLabel(data.TempInstruction, TempInstructionNullErrorLabel, "TempInstruction can not be Null");
        ValidateAndSetErrorLabel(data.TempInDegreeCelcius, TempInDegreeCelciusNullErrorLabel, "TempInDegreeCelcius can not be Null");
        ValidateAndSetErrorLabel(data.Seal1, Seal1NullErrorLabel, "Seal1 can not be Null");
        ValidateAndSetErrorLabel(data.Seal2, Seal2NullErrorLabel, "Seal2 can not be Null");

        /// If all fields are valid, proceed with saving the product
        
        if (!string.IsNullOrEmpty(data.Customer) && !string.IsNullOrEmpty(data.BookingReference) &&
           data.ContainerNumber > 0 && data.Iso > 0 && !string.IsNullOrEmpty(data.TempInstruction) &&
           !string.IsNullOrEmpty(data.TempInDegreeCelcius) && !string.IsNullOrEmpty(data.Seal1) && !string.IsNullOrEmpty(data.Seal2))
        {
            // Execute the save product command from the ViewModel
            data.SaveProductCommand.Execute(e);
        }
    }

    /// <summary>
    /// Method to validate and set error label for string fields
    /// </summary>
    /// <param name="field">The string field to validate</param>
    /// <param name="errorLabel">The error label to set</param>
    /// <param name="errorMessage">The error message to display</param>
    private void ValidateAndSetErrorLabel(string field, Label errorLabel, string errorMessage)
    {
        if (string.IsNullOrEmpty(field))
        {
            errorLabel.Text = errorMessage;
            errorLabel.IsVisible = true;
        }
        else

        {
            errorLabel.IsVisible = false;
        }
    }

    /// <summary>
    /// Method to validate and set error label for int fields
    /// </summary>
    /// <param name="field">The int field to validate</param>
    /// <param name="errorLabel">The error label to set</param>
    /// <param name="errorMessage">The error message to display</param>
    private void ValidateAndSetintErrorLabel(int field, Label errorLabel, string errorMessage)
    {
        if (field <= 0)
        {
            errorLabel.Text = errorMessage;
            errorLabel.IsVisible = true;
        }
        else

        {
            errorLabel.IsVisible = false;
        }
    }

}
