using HarpenTech.Models.Models.ApiData;
using HarpenTech.Models.Settings;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.AppEnvironment;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.Services.Settings;
using HarpenTech.ViewModels;
using HarpenTech.Views.RecievePage;
using HarpenTech.Views.SettingsPage;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HarpenTech
{

    /// <summary>
    /// This class represents the main application entry point and is responsible for initializing and managing the application.
    /// </summary>
    public partial class App
    {
        // Services for handling application settings, environment, and navigation.
        private readonly ISettingsService _settingsService;
        private readonly IAppEnvironmentService _appEnvironmentService;
        private readonly INavigationService _navigationService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly IRequestProvider _requestProvider;
        private readonly DatabaseContext _context;
        private readonly SettingViewModel _settingViewModel;

        /// <summary>
        /// Constructor for initializing the application with required services and components.
        /// </summary>
        /// <param name="navigationService">Navigation service</param>
        /// <param name="settingsService">Settings service</param>
        /// <param name="appEnvironmentService">App environment service</param>
        /// <param name="secureStorageService">Secure storage service</param>
        /// <param name="requestProvider">Request provider service</param>
        /// <param name="context">Database context</param>
        /// <param name="settingViewModel">Setting view model</param>
        public App(INavigationService navigationService, ISettingsService settingsService, IAppEnvironmentService appEnvironmentService, ISecureStorageService secureStorageService, IRequestProvider requestProvider, DatabaseContext context
            , SettingViewModel settingViewModel)
        {
            _settingViewModel = settingViewModel;
            _secureStorageService = secureStorageService;
            _context = context;
            _requestProvider = requestProvider;
            _settingsService = settingsService;
            _appEnvironmentService = appEnvironmentService;
            _navigationService = navigationService;
            InitializeComponent();
            CheckAndUpdateApp();
            MainPage = new AppShell(navigationService, _secureStorageService, _requestProvider, _context, _settingViewModel);
        }

        /// <summary>
        /// Load products asynchronously.
        /// </summary>
        /// <returns>True if products are loaded successfully, false otherwise.</returns>
        public async Task<bool> LoadProductsAsync()
        {
            try
            {
                IEnumerable<CmsSetting> products = await _context.GetAllAsync<CmsSetting>();

                if (products.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Folder path for storing application updates.
        /// </summary>
        private string UpdateFolderPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Updates");


        /// <summary>
        /// Obtain access token for making authenticated API requests.
        /// </summary>
        /// <returns>Access token string.</returns>
        private async Task<string> GetAccessToken()
        {
            // API authentication details.
            string tokenUrl = "https://ccms-har.harpan.co.za/connect/token";
            string username = "Tejinder";
            string password = "Inder123#";

            // Use HttpClient to request an access token.
            using (HttpClient client = new HttpClient())
            {
                // Prepare token request parameters.
                var tokenRequest = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };

                // Send token request and handle the response.
                var response = await client.PostAsync(tokenUrl, new FormUrlEncodedContent(tokenRequest));

                if (response.IsSuccessStatusCode)
                {
                    // Parse and return the access token from the response.
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonSerializer.Deserialize<TokenResponse>(responseBody);
                    return tokenData.AccessToken;
                }
                else
                {
                    // Handle the error response and return null.
                    string errorMessage = $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                    return null;
                }
            }
        }

        /// <summary>
        /// Check for application updates and initiate the update process if a new version is available.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>        
        private async Task CheckAndUpdateApp()
        {
            try
            {
                // API URL for version check.
                string apiUrl = "https://ccms-har.harpan.co.za/Device/Application/V1/Settings/Get/496f61eb-92b0-4518-b6a1-55801ead9899";

                // Obtain access token for API requests.
                string accessToken = await GetAccessToken();

                if (accessToken != null)
                {
                    // Create HttpClient with authorization header.
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                        // Make the GET request to check for updates.
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            // Process the response content.
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var apiData = JsonSerializer.Deserialize<ApiData>(responseBody);

                            // Compare versions and update if necessary.
                            if (apiData.application.version.Substring(1) != VersionTracking.CurrentVersion)
                            {
                                // Download and install the update.
                                string updateUrl = apiData.application.url;
                                await DownloadAndInstallUpdate(updateUrl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Alert", "Something went wrong while Checking AutoUpdate", "Ok");
            }
        }

        /// <summary>
        /// Download and install the application update.
        /// </summary>
        /// <param name="updateUrl">The URL of the update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task DownloadAndInstallUpdate(string updateUrl)
        {
            try
            {
                // Combine the base URL with the update URL.
                string fullUpdateUrl = "https://ccms-har.harpan.co.za" + updateUrl;

                // Download the update file to the local storage.
                string updateFilePath = Path.Combine(UpdateFolderPath, "MyAppUpdate.zip");

                // Notify the user about the update and prompt for confirmation.
                bool userWantsToUpdate = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
                    "New Version Available",
                    "A new version of the app is available. Do you want to update now?",
                    "Yes",
                    "No");

                // If the user agrees to update, open the browser to the update URL.
                if (userWantsToUpdate)
                {
                    Launcher.OpenAsync(fullUpdateUrl);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exception during the update process.
                Console.WriteLine($"HTTP request exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions during the update process.
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}