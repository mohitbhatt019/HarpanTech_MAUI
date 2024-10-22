using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using HarpenTech.Models.Settings;
using HarpenTech.NewFolder;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.Services.Settings;
using HarpenTech.ViewModels;

namespace HarpenTech.Views;

public partial class LoginView : ContentPageBase
{


    // ViewModel associated with the view
    private readonly LoginViewModel _loginViewModel;

    private readonly DatabaseContext _context;

    // Service for secure storage (assuming this is dependency injected)
    private readonly ISecureStorageService _secureStorageService;
    private readonly ISettingsService _settingsService;

    /// <summary>
    /// Constructor for the LoginView
    /// </summary>
    /// <param name="loginViewModel">The view model for login</param>
    /// <param name="secureStorageService">The service for secure storage</param>
    /// <param name="context">The database context</param>
    /// <param name="settingsService">The settings service</param>

    public LoginView(LoginViewModel loginViewModel, ISecureStorageService secureStorageService, DatabaseContext context, ISettingsService settingsService)
    {
        _context = context;
        LoadProductsAsync();

        _settingsService = settingsService;
        // Set the ViewModel for data binding
        BindingContext = _loginViewModel = loginViewModel;

        // Set the secure storage service
        _secureStorageService = secureStorageService;

        // Initialize the view components
        InitializeComponent();
        _loginViewModel.Appversion = VersionTracking.CurrentVersion;
        _loginViewModel.FormattedAppVersion = $"Ver. {_loginViewModel.Appversion}";
        _loginViewModel.ReleaseDate = new DateOnly(24, 01, 31);
        _loginViewModel.FormattedReleaseDate = $"Rel. {_loginViewModel.ReleaseDate:dd MMM yy}";
    }

    /// <summary>
    /// Load products asynchronously from the database
    /// </summary>
    public async Task LoadProductsAsync()
    {
        try
        {
            IEnumerable<CmsSetting> products = await _context.GetAllAsync<CmsSetting>();

            if (products.Count() != 0)
            {
                CmsSetting cmsSetting = products.LastOrDefault();
                _loginViewModel.MainDb = $"{cmsSetting.MainDb}/{cmsSetting.Depot}";
                _loginViewModel.Depot = cmsSetting.Depot;
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    /// <summary>
    /// Clear secure storage when the page appears
    /// </summary>
    protected override void OnAppearing()
    {
        ClearSecureStorage();
        base.OnAppearing();
    }

    /// <summary>
    /// Clear secure storage data
    /// </summary>
    public async Task ClearSecureStorage()
    {
        _settingsService.AuthAccessToken = string.Empty;
        _settingsService.AuthIdToken = string.Empty;
        _secureStorageService.RemoveToken("AccessToken");
        _secureStorageService.RemoveLastUpdateDateTimeOffSet("LastUpdateDateTimeOffSet");
    }

    /// <summary>
    /// Event handler for the "Login" button click
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void LoginClick(object sender, EventArgs e)
    {

        if (username.Text == null)
        {
            #region SnackBar

            await KeyboardExtensions.HideKeyboardAsync(username, default);
            await KeyboardExtensions.HideKeyboardAsync(password, default);

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Black,
                CornerRadius = new CornerRadius(0, 0, 0, 0),
                Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                CharacterSpacing = 0,

            };

            string text = " ! Username Is Required";
            string actionButtonText = "";
            Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
            TimeSpan duration = TimeSpan.FromSeconds(5);

            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            await snackbar.Show();
            #endregion
            return;
        }

        if (password.Text == null)
        {

            #region snackbar

            await KeyboardExtensions.HideKeyboardAsync(username, default);
            await KeyboardExtensions.HideKeyboardAsync(password, default);

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Black,
                CornerRadius = new CornerRadius(0, 00, 00, 00),
                Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                CharacterSpacing = 0

            };

            string text = " !  Password Is Required";
            string actionButtonText = "";
            Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            await snackbar.Show();

            #endregion

            return;
        }



        if (!NameValidator.IsValid)
        {
            if (NameValidator.Value.Length <= 0)
            {

                #region Snackbar
                await KeyboardExtensions.HideKeyboardAsync(username, default);
                await KeyboardExtensions.HideKeyboardAsync(password, default);

                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.Black,
                    CornerRadius = new CornerRadius(0, 00, 00, 00),
                    Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                    ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                    CharacterSpacing = 0,

                };

                string text = "!  Username Is Required";
                string actionButtonText = "";
                Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(4);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await snackbar.Show();
                #endregion


                return;
            }
            else
            {


                #region Snackbar
                await KeyboardExtensions.HideKeyboardAsync(username, default);
                await KeyboardExtensions.HideKeyboardAsync(password, default);

                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.Black,
                    CornerRadius = new CornerRadius(0, 00, 00, 00),
                    Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                    ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                    CharacterSpacing = 0,

                };

                string text = "!  Username Should Min 3 char long";
                string actionButtonText = "";
                Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(4);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await snackbar.Show();
                #endregion

                return;
            }

        }

        if (!PasswordValidator.IsValid)
        {
            if (PasswordValidator.Value.Length <= 0)
            {
                #region Snackbar
                await KeyboardExtensions.HideKeyboardAsync(username, default);
                await KeyboardExtensions.HideKeyboardAsync(password, default);

                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.Black,
                    CornerRadius = new CornerRadius(0, 00, 00, 00),
                    Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                    ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                    CharacterSpacing = 0,

                };

                string text = "!  Password Is Required";
                string actionButtonText = "";
                Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(4);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await snackbar.Show();
                #endregion

                return;
            }
            else
            {

                #region Snackbar
                await KeyboardExtensions.HideKeyboardAsync(username, default);
                await KeyboardExtensions.HideKeyboardAsync(password, default);

                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.Black,
                    CornerRadius = new CornerRadius(0, 00, 00, 00),
                    Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                    ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                    CharacterSpacing = 0,

                };

                string text = "! Password Require UpperCase,LowerCase and Speacial char";
                string actionButtonText = "";
                Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(4);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await snackbar.Show();
                #endregion

                return;
            }
        }

        var data = (LoginViewModel)BindingContext;

        if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password))
        {
            // Hide the keyboard
            await KeyboardExtensions.HideKeyboardAsync(username, default);
            await KeyboardExtensions.HideKeyboardAsync(password, default);


            // Execute the login command from the ViewModel
            ((LoginViewModel)BindingContext).LoginClickAsyncCommand.Execute(e);
        }
    }

    /// <summary>
    /// Event handler for the "Cancel" button click
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private async void CancelClick(object sender, EventArgs e)
    {

        //Display a confirmation dialog for quitting the app
        bool result = await App.Current.MainPage.DisplayAlert("Quit", "You sure want to close the app?", "Yes", "Cancel");

        //If user chooses to quit, exit the app
        if (result) App.Current.Quit();

    }

    /// <summary>
    /// Event handler when Password Entry is Focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void PasswordLabelFocus(object sender, FocusEventArgs e)
    {
        PasswordClearText.IsVisible = true;
        PasswordLabel.IsVisible = true;
        PeekPassowordTextVisible.IsVisible = true;
    }

    /// <summary>
    /// Event handler when Password Entry is UnFocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void PasswordLabelUnFocus(object sender, FocusEventArgs e)
    {
        if (password.Text == null || password.Text == "")
        {
            PasswordLabel.IsVisible = false;
            PasswordClearText.IsVisible = false;
            PeekPassowordTextVisible.IsVisible = false;
            PeekPassowordTextHide.IsVisible = false;
        }
        else
        {
            PasswordClearText.IsVisible = false;
            PasswordLabel.IsVisible = true;
            PeekPassowordTextVisible.IsVisible = false;
            password.IsPassword = true;



        }
    }

    /// <summary>
    /// Event handler when Name Entry  is Focused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserNameLabelFocus(object sender, FocusEventArgs e)
    {
        UserLabel.IsVisible = true;
        UserNameClearText.IsVisible = true;
    }

    /// <summary>
    /// Event handler when Name Entry is UnFocused
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">The event arguments</param>
    private void UserNameLabelUnFocus(object sender, FocusEventArgs e)
    {
        if (username.Text == null || username.Text == "")
        {
            UserLabel.IsVisible = false;
            UserNameClearText.IsVisible = false;
        }
        else UserLabel.IsVisible = true;
        PeekPassowordTextHide.IsVisible = false;
        PeekPassowordTextVisible.IsVisible = false;
        UserNameClearText.IsVisible = false;




    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PeekPasswordClick(object sender, EventArgs e)
    {
        if (password.IsPassword)
        {
            password.IsPassword = false;
            PeekPassowordTextVisible.IsVisible = false;
            PeekPassowordTextHide.IsVisible = true;
        }
        else
        {
            password.IsPassword = true;
            PeekPassowordTextVisible.IsVisible = true;
            PeekPassowordTextHide.IsVisible = false;
        }
    }


    /* Custom ClearTextVisibility whileEditing UserName */
    private void UserClearTextEditing(object sender, EventArgs e)
    {
        username.Text = string.Empty;
    }

    /* Custom ClearTextVisibility whileEditing Password */
    private void PasswordClearTextEditing(object sender, EventArgs e)
    {
        password.Text = string.Empty;
    }
}



