using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using HarpenTech.NewFolder;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;

namespace HarpenTech.Views.SettingsPage;

public partial class SettingView : ContentPageBase
{

    private readonly SettingViewModel _settingViewModel;
    private readonly DatabaseContext _context;
    private readonly ISecureStorageService _secureStorageService;


    /// <summary>
    /// Constructor for SettingView
    /// </summary>
    /// <param name="settingViewModel">The ViewModel for data binding</param>
    /// <param name="context">The database context</param>
    /// <param name="secureStorageService">The service for secure storage</param>
    public SettingView(SettingViewModel settingViewModel, DatabaseContext context, ISecureStorageService secureStorageService)
    {
        _context = context;
        _settingViewModel = settingViewModel;
        _secureStorageService = secureStorageService;
        BindingContext = _settingViewModel;
        InitializeComponent();
    }

    /// <summary>
    /// Event handler when Item is Focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserURLFocus(object sender, FocusEventArgs e)
    {
        UserURL.IsVisible = true;
    }

    /// <summary>
    /// Event handler when UserURL Entry is UnFocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserURLUnfocus(object sender, FocusEventArgs e)
    {
        UserURL.IsVisible = false;
    }

    /// <summary>
    /// Event handler when UserCompany Entry is Focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserCompanyNameFocus(object sender, FocusEventArgs e)
    {
        UserCompanyName.IsVisible = true;
    }

    /// <summary>
    /// Event handler when UserCompany Entry is UnFocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserCompanyNameUnfocus(object sender, FocusEventArgs e)
    {
        UserCompanyName.IsVisible = false;
    }

    /// <summary>
    /// Event handler when UserDepot Entry is UnFocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserDepotNameUnfocus(object sender, FocusEventArgs e)
    {
        UserDepot.IsVisible = false;
    }

    /// <summary>
    /// Event handler when UserDepot Entry is Focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserDepotNameFocus(object sender, FocusEventArgs e)
    {
        UserDepot.IsVisible = true;
    }

    /// <summary>
    /// Event handler when UserMain Entry is UnFocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserMainDbUnfocus(object sender, FocusEventArgs e)
    {
        UserMainDb.IsVisible = false;
    }

    /// <summary>
    /// Event handler when UserMain is Focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserMainDbFocus(object sender, FocusEventArgs e)
    {
        UserMainDb.IsVisible = true;
    }

    /// <summary>
    /// To Execute when click on CancelClick button.
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void CancelClick(object sender, EventArgs e)
    {
        // Retrieve the saved access token from secure storage

        // Display a confirmation dialog for quitting the app
        bool result = await App.Current.MainPage.DisplayAlert("Quit", "You sure want to close the app?", "Yes", "Cancel");


        // If user chooses to quit, exit the app
        if (result) App.Current.Quit();

    }


    /// <summary>
    /// Event handler when the "Save" button is clicked.
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void SaveClick(object sender, EventArgs e)
    {




        if (_settingViewModel.Url == null || _settingViewModel.Url == "" || _settingViewModel.CompanyName == null || _settingViewModel.CompanyName == "" || _settingViewModel.MainDb == null || _settingViewModel.MainDb == "" || _settingViewModel.Depot == null || _settingViewModel.Depot == "")
        {

            #region Snackbar

            await KeyboardExtensions.HideKeyboardAsync(userentry, default);
            await KeyboardExtensions.HideKeyboardAsync(companyentry, default);
            await KeyboardExtensions.HideKeyboardAsync(mainentry, default);
            await KeyboardExtensions.HideKeyboardAsync(depotentry, default);

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Black,
                CornerRadius = new CornerRadius(0, 00, 00, 00),
                Font = Microsoft.Maui.Font.SystemFontOfSize(10),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                CharacterSpacing = 0.2

            };
            string text = "! All Fields are required";
            string actionButtonText = "";
            Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
            TimeSpan duration = TimeSpan.FromSeconds(4);

            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
            await snackbar.Show();

            #endregion

        }

        else
        {

            await ((SettingViewModel)BindingContext).SaveClick1TimeCommand.ExecuteAsync(e);

        }

    }
}