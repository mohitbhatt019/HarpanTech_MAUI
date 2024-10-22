using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using HarpenTech.NewFolder;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;

namespace HarpenTech.Views.SettingsPage;

public partial class SettingPage2 : ContentPageBase
{
    private readonly SettingViewModel _settingViewModel;

    private readonly DatabaseContext _context;
    private readonly ISecureStorageService _secureStorageService;

    /// <summary>
    /// Constructor for SettingPage2
    /// </summary>
    /// <param name="settingViewModel">The ViewModel for data binding</param>
    /// <param name="context">The database context</param>
    /// <param name="secureStorageService">The service for secure storage</param>
    public SettingPage2(SettingViewModel settingViewModel, DatabaseContext context, ISecureStorageService secureStorageService)
    {
        _context = context;
        _secureStorageService = secureStorageService;
        BindingContext = _settingViewModel = settingViewModel;
        InitializeComponent();
    }


    /// <summary>
    /// Event handler when the UserURL entry is focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserURLFocus(object sender, FocusEventArgs e)
    {
        // Make the UserURL label visible
        UserURL.IsVisible = true;
    }

    /// <summary>
    /// Event handler when the UserURL entry is unfocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param> name="e">The event arguments</param>
    private void UserURLUnfocus(object sender, FocusEventArgs e)
    {
        // Hide the UserURL label
        UserURL.IsVisible = false;
    }

    /// <summary>
    /// Event handler when the UserCompanyName entry is focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserCompanyNameFocus(object sender, FocusEventArgs e)
    {
        // Make the UserCompanyName label visible
        UserCompanyName.IsVisible = true;
    }

    /// <summary>
    /// Event handler when the UserCompanyName entry is unfocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserCompanyNameUnfocus(object sender, FocusEventArgs e)
    {
        // Hide the UserCompanyName label
        UserCompanyName.IsVisible = false;
    }

    /// <summary>
    /// Event handler when the UserDepot entry is focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserDepotNameFocus(object sender, FocusEventArgs e)
    {
        // Make the UserDepot label visible
        UserDepot.IsVisible = true;
    }

    /// <summary>
    /// Event handler when the UserDepot entry is unfocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserDepotNameUnfocus(object sender, FocusEventArgs e)
    {
        // Hide the UserDepot label
        UserDepot.IsVisible = false;
    }

    /// <summary>
    /// Event handler when the UserMainDb entry is focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserMainDbFocus(object sender, FocusEventArgs e)
    {
        // Make the UserMainDb label visible
        UserMainDb.IsVisible = true;
    }

    /// <summary>
    /// Event handler when the UserMainDb entry is unfocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserMainDbUnfocus(object sender, FocusEventArgs e)
    {
        // Hide the UserMainDb label
        UserMainDb.IsVisible = false;
    }

    /// <summary>
    /// Event handler when the "Cancel" button is clicked
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void CancelClick(object sender, EventArgs e)
    {
        // Retrieve the saved access token from secure storage
        var saveAccessToken = await _secureStorageService.GetToken("AccessToken");

        // Display a confirmation dialog for quitting the app
        bool result = await App.Current.MainPage.DisplayAlert("Quit", "Are you sure you want to close the app?", "Yes", "Cancel");

        // If the user chooses to quit, exit the app
        if (result) App.Current.Quit();
    }

    /// <summary>
    /// Event handler when the "Save" button is clicked
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void SaveClick(object sender, EventArgs e)
    {
        // Execute the save command from the ViewModel

        await Task.Delay(1000);



        if (_settingViewModel.MainDb == null || _settingViewModel.MainDb == "" || _settingViewModel.Depot == null || _settingViewModel.Depot == "")
        {   

            #region Snackbar

            await KeyboardExtensions.HideKeyboardAsync(userentry, default);
            await KeyboardExtensions.HideKeyboardAsync(companyentry, default);
            await KeyboardExtensions.HideKeyboardAsync(mainentry, default);
            await KeyboardExtensions.HideKeyboardAsync(depoentry, default);

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Black,
                CornerRadius = new CornerRadius(0, 00, 00, 00),
                Font = Microsoft.Maui.Font.SystemFontOfSize(10),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                CharacterSpacing = 0.2

            };
            string text = "! MainDb and Depot are required fields";
            string actionButtonText = "";
            Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
            TimeSpan duration = TimeSpan.FromSeconds(4);

            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
            await snackbar.Show();

            #endregion

        }

        else
        {
             await KeyboardExtensions.HideKeyboardAsync(userentry, default);
            await KeyboardExtensions.HideKeyboardAsync(companyentry, default);
            await KeyboardExtensions.HideKeyboardAsync(mainentry, default);
            await KeyboardExtensions.HideKeyboardAsync(depoentry, default);
            await ((SettingViewModel)BindingContext).SaveClickCommand.ExecuteAsync(e);

        }


    }
}