using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using HarpenTech.Models.Container;
using HarpenTech.NewFolder;
using HarpenTech.Services.PartialMethods;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using HarpenTech.ViewModels.Base;
using HarpenTech.Views.datascreen;

namespace HarpenTech.Views.RecievePage;

public partial class RecieveEditView : ContentPageBase
{
    private readonly IRequestProvider _requestProvider;
    private readonly DatabaseContext _context;
    private readonly ContainerInfo _containerItem;
    private readonly ISecureStorageService _secureStorage;

    // Constructor for the view
    public RecieveEditView(IRequestProvider requestProvider, ContainerInfo containerItem, DatabaseContext context, ISecureStorageService secureStorage)
    {
        _secureStorage = secureStorage;
        _requestProvider = requestProvider;
        InitializeComponent();
        // Set the ViewModel for data binding
        BindingContext = new RecieveEditViewModel(containerItem, requestProvider,null, context, _secureStorage);
        _context = context;
        _containerItem = containerItem;
    }

    /// <summary>
    /// Event handler for Cancel button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void CancelClickCommand(object sender, EventArgs e)
    {
        // Retrieve ViewModel from the binding context
        var viewModel = (RecieveEditViewModel)BindingContext;

        // Navigate back
        await Shell.Current.Navigation.PopAsync();
    }

    // Event handler for Save button click
    private async void SaveClickCommand(object sender, EventArgs e)
    {
        #region Close keyboard
        await KeyboardExtensions.HideKeyboardAsync(ContainerNumber, default);
        await KeyboardExtensions.HideKeyboardAsync(Customer, default);
        await KeyboardExtensions.HideKeyboardAsync(remarksEditor, default);
        #endregion

        // Execute the save command from the ViewModel
        ((RecieveEditViewModel)BindingContext).SaveClickCommand.Execute(e);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void DisplayDataClick(object sender, EventArgs e)
    {
        #region Close keyboard
        await KeyboardExtensions.HideKeyboardAsync(ContainerNumber, default);
        await KeyboardExtensions.HideKeyboardAsync(Customer, default);
        await KeyboardExtensions.HideKeyboardAsync(remarksEditor, default);
        #endregion

        await Shell.Current.Navigation.PushAsync(new ReceieveViewData(_context, _containerItem, _requestProvider, _secureStorage));
    }

    /// <summary>
    /// Execute when click on save button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void SaveProduct(object sender, EventArgs e)
    {
        try
        {
            var data = (RecieveEditViewModel)BindingContext;
            #region Validation
            ValidateAndSetErrorLabel(data.ContainerNumber, ContainerNullErrorLabel, "Container can not be Null");
            ValidateAndSetErrorLabel(data.Remarks, RemarksNullErrorLabel, "Please add Remarks");
            ValidateAndSetErrorLabel(data.Customer, CustomerNullErrorLabel, "Customer can not be Null");
            #endregion

            // If all fields are valid, proceed with saving the product
            if (!string.IsNullOrEmpty(data.Customer) && !string.IsNullOrEmpty(data.Remarks) && !string.IsNullOrEmpty(data.ContainerNumber))
            {
                // Execute the save product command from the ViewModel
                data.SaveProductAsync();
            }
        }
        catch (Exception)
        {
            #region Toster
            string text = "Entry Not Saved";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 20;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show();
            #endregion
        }

    }

    /// <summary>
    /// To Validate and Set the ErrorLabel
    /// </summary>
    /// <param name="field"></param>
    /// <param name="errorLabel"></param>
    /// <param name="errorMessage"></param>
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
}
