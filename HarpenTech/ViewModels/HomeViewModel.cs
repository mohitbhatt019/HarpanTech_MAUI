using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Services;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.Services.Settings;
using HarpenTech.ViewModels.Base;

namespace HarpenTech.ViewModels
{
    // Partial class for the HomeViewModel, extending ViewModelBase
    public partial class HomeViewModel : ViewModelBase
    {
        // Private fields to store services
        private readonly ISettingsService _settingsService;
        private readonly ISecureStorageService _secureStorageService;

        // Constructor for HomeViewModel, taking INavigationService, ISettingsService, and ISecureStorageService as parameters
        public HomeViewModel(INavigationService navigationService, ISettingsService settingsService, ISecureStorageService secureStorageService) : base(navigationService)
        {
            // Initialize services
            _settingsService = settingsService;
            _secureStorageService = secureStorageService;
        }

        public async Task ClearSecureStorage()
        {
            _settingsService.AuthAccessToken = string.Empty;
            _settingsService.AuthIdToken = string.Empty;
            _secureStorageService.RemoveToken("AccessToken");
            _secureStorageService.RemoveLastUpdateDateTimeOffSet("LastUpdateDateTimeOffSet");
        }

        /// <summary>
        /// RelayCommand method for handling the logout button click
        /// </summary>
        [RelayCommand]
        private async Task LogoutAsync()
        {
            await IsBusyFor(
            async () =>
            {

                // Get the current navigation stack
                var stack = Application.Current.MainPage.Navigation.NavigationStack.ToArray();

                try
                {
                    await ClearSecureStorage();

                    for (int i = stack.Length - 1; i > 0; i--)
                    {
                        Application.Current.MainPage.Navigation.RemovePage(stack[i]);
                    }

                    var date = await _secureStorageService.GetLastUpdateDateTimeOffSet("LastUpdateDateTimeOffSet");

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

                    string text = " \u2713 User Logged Out Successfully";
                    string actionButtonText = "";
                    Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                    TimeSpan duration = TimeSpan.FromSeconds(4);

                    var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
                    await Task.Delay(2000);
                    await snackbar.Show();
                    #endregion

                    // Navigate to the login page
                    await NavigationService.NavigateToAsync("//login");
                }
                catch (Exception ex)
                {
                    // Throw any exceptions that occur during the logout process
                    throw ex;
                }
            });
        }

        /// <summary>
        /// RelayCommand method for handling the receive button click
        /// </summary>
        [RelayCommand]
        private async Task RecieveAsync()
        {
            await IsBusyFor(
            async () =>
            {
                try
                {
                    await NavigationService.NavigateToAsync("//Recieve");
                }
                catch (Exception ex)
                {
                    #region Toster
                    string text = "Something gets wrong while navigating";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 20;

                    var toast = Toast.Make(text, duration, fontSize);

                    await toast.Show();
                    #endregion

                }
            });
        }

        /// <summary>
        /// RelayCommand method for handling the dispatch button click
        /// </summary>
        [RelayCommand]
        private async Task DispatchAsync()
        {
            await IsBusyFor(
            async () =>
            {
                // Navigate asynchronously to the Dispatch page
                await NavigationService.NavigateToAsync("//Dispatch");
            });
        }

        /// <summary>
        /// RelayCommand method for handling the repair button click
        /// </summary>
        [RelayCommand]
        private async Task RepairAsync()
        {
            await IsBusyFor(
            async () =>
            {
                // Navigate asynchronously to the Repair page
                await NavigationService.NavigateToAsync("//Repair");
            });
        }
    }
}
