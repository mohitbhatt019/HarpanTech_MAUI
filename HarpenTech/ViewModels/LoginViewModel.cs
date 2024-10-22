using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.Services.Settings;
using HarpenTech.ViewModels.Base;

namespace HarpenTech.ViewModels;

// Partial class for the LoginViewModel, extending ViewModelBase
public partial class LoginViewModel : ViewModelBase
{
    // Private fields to store services and observable properties
    private readonly ISettingsService _settingsService;
    private readonly IRequestProvider _requestProvider;
    private readonly ISecureStorageService _secureStorageService;

    #region Observable properties

    // Observable property for the username
    [ObservableProperty]
    string _userName;

    // Observable property for the password
    [ObservableProperty]
    string _password;

    // Observable property for tracking the login status
    [ObservableProperty]
    private bool _isLogin;

    // Observable property for storing the login URL
    [ObservableProperty]
    private string _loginUrl;

    // Observable property for AppVersion
    [ObservableProperty]
    private string _appversion;

    // Observable property for ReleaseDate
    [ObservableProperty]
    private DateOnly _releaseDate;

    // Observable property for FormatedReleaseDate
    [ObservableProperty]
    private string _formattedReleaseDate;

    // Observable property for FormattedAppVersion
    [ObservableProperty]
    private string _formattedAppVersion;

    // Observable property for MainDb
    [ObservableProperty]
    private string _mainDb;

    // Observable property for Depot
    [ObservableProperty]
    private string _depot;

    #endregion

    // Constructor for the LoginViewModel, taking services as parameters
    public LoginViewModel(INavigationService navigationService, ISettingsService settingsService, IRequestProvider requestProvider, ISecureStorageService secureStorageService)
        : base(navigationService)
    {
        // Initialize services
        _settingsService = settingsService;
        _requestProvider = requestProvider;
        _requestProvider.SetBaseAddress(GlobalSetting.ApiBaseUrl);
        _secureStorageService = secureStorageService;
    }


    // Override method to apply query attributes
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        base.ApplyQueryAttributes(query);
    }

    // Override method to initialize the ViewModel asynchronously
    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronous method for handling the login button click event.
    /// </summary>
    [RelayCommand]
    private async void LoginClickAsync()
    {
        try
        {
            if (Device.RuntimePlatform == Device.Android)
            {

                await IsBusyFor(
               async () =>
               {
                   // Create a new instance of RequestProvider
                   RequestProvider requestProvider = new RequestProvider();

                   // Prepare form content for login request
                   var formContent = new System.Net.Http.FormUrlEncodedContent(new[]
                   {
                            new KeyValuePair<string, string>("username", UserName),
                            new KeyValuePair<string, string>("password", Password),
                            new KeyValuePair<string, string>("client_id", _settingsService.AuthClient_id),
                            new KeyValuePair<string, string>("grant_type", _settingsService.AuthGrant_type),
                            new KeyValuePair<string, string>("scope", _settingsService.AuthOffline_access)
                       });

                   // Define the API endpoint for token request
                   string api = "connect/token";

                   // Post login request and get the token response
                   TokenResponse response = _requestProvider.PostAsFormUrlEncoded<TokenResponse, System.Net.Http.FormUrlEncodedContent>(api, formContent);

                   // Check if the access token is not empty
                   if (!string.IsNullOrEmpty(response.AccessToken))
                   {
                       // Clear username and password, set access token, and save it securely
                       UserName = "";
                       Password = "";
                       _settingsService.AuthAccessToken = response.AccessToken;
                       _settingsService.AuthIdToken = response.IdToken;
                       _secureStorageService.SaveToken("AccessToken", response.AccessToken);

                       #region Snackbar

                       var snackbarOptions = new SnackbarOptions
                       {
                           BackgroundColor = Colors.LightGray,
                           TextColor = Colors.Black,
                           CornerRadius = new CornerRadius(0, 00, 00, 00),
                           Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                           ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                           CharacterSpacing = 0.2

                       };

                       string text = " \u2713 User Logged in Successfully";
                       string actionButtonText = "";
                       Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                       TimeSpan duration = TimeSpan.FromSeconds(2);

                       var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                       await Task.Delay(2000);
                       await snackbar.Show();
                       #endregion

                       // Navigate to the home page
                       await NavigationService.NavigateToAsync("//HomePage");
                   }
                   else
                   {
                       #region Snackbar

                       var snackbarOptions = new SnackbarOptions
                       {
                           BackgroundColor = Colors.Red,
                           TextColor = Colors.Black,
                           CornerRadius = new CornerRadius(0, 00, 00, 00),
                           Font = Microsoft.Maui.Font.SystemFontOfSize(10),
                           ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                           CharacterSpacing = 0.2

                       };
                       string text = " ! Login failed. Please check your credentials";
                       string actionButtonText = "";
                       Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                       TimeSpan duration = TimeSpan.FromSeconds(4);

                       var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
                       await snackbar.Show();

                       #endregion
                   }

               });
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {

                await IsBusyFor(
               async () =>
               {
                   // Create a new instance of RequestProvider
                   RequestProvider requestProvider = new RequestProvider();

                   // Prepare form content for login request
                   var formContent = new System.Net.Http.FormUrlEncodedContent(new[]
                   {
                            new KeyValuePair<string, string>("password", Password),
                            new KeyValuePair<string, string>("client_id", _settingsService.AuthClient_id),
                            new KeyValuePair<string, string>("grant_type", _settingsService.AuthGrant_type),
                            new KeyValuePair<string, string>("scope", _settingsService.AuthOffline_access)
                       });

                   // Define the API endpoint for token request
                   string api = "connect/token";

                   // Post login request and get the token response
                   TokenResponse response = _requestProvider.PostAsFormUrlEncoded<TokenResponse, System.Net.Http.FormUrlEncodedContent>(api, formContent);

                   // Check if the access token is not empty
                   if (!string.IsNullOrEmpty(response.AccessToken))
                   {
                       // Clear username and password, set access token, and save it securely
                       UserName = "";
                       Password = "";
                       _settingsService.AuthAccessToken = response.AccessToken;
                       _settingsService.AuthIdToken = response.IdToken;
                       _secureStorageService.SaveToken("AccessToken", response.AccessToken);

                       // Navigate to the home page
                       await NavigationService.NavigateToAsync("//HomePage");
                   }
                   else
                   {
                       #region Toster
                       string text = "Login Failed,Please Check Your Credentials";
                       ToastDuration duration = ToastDuration.Short;
                       double fontSize = 20;

                       var toast = Toast.Make(text, duration, fontSize);

                       await toast.Show();
                       #endregion
                   }

               });
            }



            else if (Device.RuntimePlatform == Device.WinUI)
            {

                await IsBusyFor(
               async () =>
               {
                   // Create a new instance of RequestProvider
                   RequestProvider requestProvider = new RequestProvider();

                   // Prepare form content for login request
                   var formContent = new System.Net.Http.FormUrlEncodedContent(new[]
                   {
                            new KeyValuePair<string, string>("username", UserName),
                            new KeyValuePair<string, string>("password", Password),
                            new KeyValuePair<string, string>("client_id", "Harpan.Web"),
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("scope", "offline_access")
                       });

                   // Define the API endpoint for token request
                   string api = "connect/token";

                   // Post login request and get the token response
                   TokenResponse response = _requestProvider.PostAsFormUrlEncoded<TokenResponse, System.Net.Http.FormUrlEncodedContent>(api, formContent);

                   // Check if the access token is not empty
                   if (!string.IsNullOrEmpty(response.AccessToken))
                   {
                       // Clear username and password, set access token, and save it securely
                       UserName = "";
                       Password = "";
                       _settingsService.AuthAccessToken = response.AccessToken;
                       _settingsService.AuthIdToken = response.IdToken;
                       _secureStorageService.SaveToken("AccessToken", response.AccessToken);

                       #region Toster
                       string text = "User Logged in Successfully";
                       ToastDuration duration = ToastDuration.Short;
                       double fontSize = 20;

                       var toast = Toast.Make(text, duration, fontSize);
                       await Task.Delay(2000);
                       await toast.Show();
                       #endregion
                       // Navigate to the home page
                       await NavigationService.NavigateToAsync("//HomePage");
                   }
                   else
                   {
                       #region Toster
                       string text = "Login Failed,Please Check Your Credentials";
                       ToastDuration duration = ToastDuration.Short;
                       double fontSize = 20;

                       var toast = Toast.Make(text, duration, fontSize);

                       await toast.Show();
                       #endregion
                   }

               });
            }



        }
        catch (Exception ex)
        {
            #region Toster
            string text = "Something Went Wrong, Check Your Internet Connection";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 17;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show();
            #endregion

        }
    }

    /// <summary>
    /// Asynchronous method for handling the setting button click event.
    /// </summary>
    [RelayCommand]
    private async void SettingClick()
    {
        // Navigate to the settings page
        await NavigationService.NavigateToAsync("//Settings/details");
    }

}

