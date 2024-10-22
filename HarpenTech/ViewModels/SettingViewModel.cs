using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models.Settings;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.ViewModels.Base;
using System.Collections.ObjectModel;

namespace HarpenTech.ViewModels
{
    public partial class SettingViewModel : ViewModelBase
    {
        private readonly DatabaseContext _context;
        private readonly IRequestProvider _requestProvider;


        #region Observable Properties
        // Observable property for the URL
        [ObservableProperty]
        string _url;

        // Observable property for the CompanyName
        [ObservableProperty]
        string _companyName;

        // Observable property for the CompanyName
        [ObservableProperty]
        string _mainDb;

        // Observable property for the CompanyName
        [ObservableProperty]
        string _depot;

        [ObservableProperty]
        private CmsSetting _operatingProduct = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;


        [ObservableProperty]
        public ObservableCollection<CmsSetting> _products = new();

        #endregion

        /// <summary>
        /// ViewModel for managing settings related to products.
        /// </summary>
        /// <param name="navigationService">Navigation service for navigating between pages.</param>
        /// <param name="context">Database context for interacting with the database.</param>
        /// <param name="requestProvider">Service for making HTTP requests.</param>
        public SettingViewModel(INavigationService navigationService, DatabaseContext context, IRequestProvider requestProvider) : base(navigationService)
        {
            _context = context;
            _requestProvider = requestProvider;
            _requestProvider.SetBaseAddress("https://659ce25b633f9aee79081664.mockapi.io/");


            LoadProductsAsync();
        }

        /// <summary>
        /// Command to set the operating product based on the provided product or create a new one.
        /// </summary>
        /// <param name="product">The product to be set as the operating product, nullable.</param>
        [RelayCommand]
        private void SetOperatingProduct(CmsSetting? product) => OperatingProduct = product ?? new();


        /// <summary>
        /// Command to save the changes made in the UI to the operating product.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        [RelayCommand]
        private async Task SaveClickAsync()
        {
            try
            {
                // Set default values for URL and CompanyName
                Url = "https://659ce25b633f9aee79081664.mockapi.io/ccms-har-harpan-co-za/company";
                CompanyName = "harpan";



                #region Mapping
                OperatingProduct.Url = _url;
                OperatingProduct.CompanyName = _companyName;
                OperatingProduct.MainDb = _mainDb;
                OperatingProduct.Depot = _depot;
                #endregion

                #region Save
                var busyText = OperatingProduct.Url.Length <= 0 ? "Creating product..." : "Updating product...";

                //await ExecuteAsync(async () =>
                //{

                #region Mapping
                OperatingProduct.Url = _url;
                OperatingProduct.CompanyName = _companyName;
                OperatingProduct.MainDb = _mainDb;
                OperatingProduct.Depot = _depot;
                OperatingProduct.Title = "Add";
                OperatingProduct.IsComplete = true;
                #endregion
                // Check if any of the required properties is null
                List<string> emptyFields = new List<string>();

                var (isValid, errorMessage) = OperatingProduct.Validate();
                if (!isValid)
                {
                    await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                    return;
                }

                if (OperatingProduct.Url != null && OperatingProduct.CompanyName != null && OperatingProduct.MainDb != null && OperatingProduct.Depot != null)
                {
                    // Create product
                    await _context.AddItemAsync<CmsSetting>(OperatingProduct);
                    Products.Add(OperatingProduct);

                    #region Snackbar

                    await KeyboardExtensions.HideKeyboardAsync(default, default);

                    var snackbarOptions = new SnackbarOptions
                    {
                        BackgroundColor = Colors.LightGray,
                        TextColor = Colors.Black,
                        CornerRadius = new CornerRadius(0, 00, 00, 00),
                        Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                        ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                        CharacterSpacing = 0.2

                    };
                    string text = "✓ Data saved Successfully";
                    string actionButtonText = "";
                    Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                    TimeSpan duration = TimeSpan.FromSeconds(4);

                    var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
                    await snackbar.Show();

                    #endregion

                    Url = string.Empty;
                    CompanyName = string.Empty;
                    MainDb = string.Empty;
                    Depot = string.Empty;
                }


                #endregion

            }

            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Alert", ex.Message, "Error");
            }
        }

        /// <summary>
        /// Asynchronously loads products from the database.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        public async Task LoadProductsAsync()
        {

            // await Task.Delay(2000);

            var products = await _context.GetAllAsync<CmsSetting>();
            if (products is not null && products.Any())
            {
                Products ??= new ObservableCollection<CmsSetting>();

                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
        }

        /// <summary>
        /// Command to test the connection asynchronously.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        [RelayCommand]
        public async Task testConnectionClickAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(2000);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Alert", ex.Message, "Ok");
            }
            IsBusy = false;
        }

        /// <summary>
        /// Command to navigate back to the HomePage asynchronously.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        [RelayCommand]
        public async Task BackClickAsync()
        {
            await NavigationService.NavigateToAsync("//HomePage");
        }

        /// <summary>
        /// Command to save changes made in the UI to the operating product with additional validation.
        /// </summary>
        /// <returns>Asynchronous task.</returns>
        [RelayCommand]
        private async Task SaveClick1TimeAsync()
        {
            await IsBusyFor(
               async () =>
               {
                   IsBusy = true;
                   await Task.Delay(1300);

                   try
                   {
                       if (Url == null || Url == "" || CompanyName == null || CompanyName == "" || MainDb == null || MainDb == "" || Depot == null || Depot == "")
                       {
                           IsBusy = false;
                           #region Snackbar

                           await KeyboardExtensions.HideKeyboardAsync(null, default);
                           await KeyboardExtensions.HideKeyboardAsync(null, default);

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
                       else
                       {
                           #region Mapping
                           OperatingProduct.Url = _url;
                           OperatingProduct.CompanyName = _companyName;
                           OperatingProduct.MainDb = _mainDb;
                           OperatingProduct.Depot = _depot;
                           #endregion

                           #region Save
                           var busyText = OperatingProduct.Url.Length <= 0 ? "Creating product..." : "Updating product...";

                           //await ExecuteAsync(async () =>
                           //{

                           #region Mapping
                           OperatingProduct.Url = _url;
                           OperatingProduct.CompanyName = _companyName;
                           OperatingProduct.MainDb = _mainDb;
                           OperatingProduct.Depot = _depot;
                           OperatingProduct.Title = "Add";
                           OperatingProduct.IsComplete = true;
                           #endregion
                           // Check if any of the required properties is null
                           List<string> emptyFields = new List<string>();

                           var (isValid, errorMessage) = OperatingProduct.Validate();

                           if (!isValid)
                           {
                               await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                               return;
                           }

                           if (OperatingProduct.Url != null && OperatingProduct.CompanyName != null && OperatingProduct.MainDb != null && OperatingProduct.Depot != null)
                           {
                               // Create product
                               await _context.AddItemAsync<CmsSetting>(OperatingProduct);
                               Products.Add(OperatingProduct);



                               #region Snackbar

                               await KeyboardExtensions.HideKeyboardAsync(null, default);

                               var snackbarOptions = new SnackbarOptions
                               {
                                   BackgroundColor = Colors.LightGray,
                                   TextColor = Colors.Black,
                                   CornerRadius = new CornerRadius(0, 00, 00, 00),
                                   Font = Microsoft.Maui.Font.SystemFontOfSize(15),
                                   ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(0),
                                   CharacterSpacing = 0.2

                               };
                               string text = "\u2713 Data saved Successfully";
                               string actionButtonText = "";
                               Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                               TimeSpan duration = TimeSpan.FromSeconds(4);

                               var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
                               await snackbar.Show();

                               #endregion

                               Url = string.Empty;
                               CompanyName = string.Empty;
                               MainDb = string.Empty;
                               Depot = string.Empty;
                               await NavigationService.NavigateToAsync("//login");
                           }

                           #endregion
                       }
                       IsBusy = false;

                   }

                   catch (Exception ex)
                   {
                       IsBusy = false;
                       await Shell.Current.DisplayAlert("Alert", ex.Message, "Error");
                   }
               });
        }



    }
}
