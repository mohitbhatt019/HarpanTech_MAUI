using HarpenTech.Models.Container;
using HarpenTech.Models.Settings;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using HarpenTech.Views;
using HarpenTech.Views.datascreen;
using HarpenTech.Views.Dispatch;
using HarpenTech.Views.HomePage;
using HarpenTech.Views.RecievePage;
using HarpenTech.Views.RepairPage;
using HarpenTech.Views.SettingsPage;
using HarpenTech.Views.Testing;

namespace HarpenTech
{
    /// <summary>
    /// Represents the main navigation shell of the application, derived from the Shell class.
    /// </summary>
    public partial class AppShell : Shell
    {
        // Service for handling navigation within the application.
        private readonly INavigationService _navigationService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly DatabaseContext _context;
        private readonly IRequestProvider _requestProvider;
        private readonly SettingViewModel _settingViewModel;


        // Constructor for initializing the AppShell with the provided navigation service.
        public AppShell(INavigationService navigationService, ISecureStorageService secureStorageService, IRequestProvider requestProvider, DatabaseContext context, SettingViewModel settingViewModel)
        {
            _navigationService = navigationService;
            _context = context;
            _requestProvider = requestProvider;
            _secureStorageService = secureStorageService;
            _settingViewModel = settingViewModel;

            // Initialize routing by registering routes for different views.
            AppShell.InitializeRouting();
            InitializeComponent();
        }


        /// <summary>
        /// This method is invoked when the Page is about to appear on the screen.
        /// </summary>
        protected async override void OnAppearing()
        {
           
            IEnumerable<CmsSetting> products = await _context.GetAllAsync<CmsSetting>();
          if (products.Count() == 0)
            {
             
                await _navigationService.NavigateToAsync("//Settings");
            }
            else
            {
            
                await _navigationService.NavigateToAsync("//login");
            }
        }



        /// <summary>
        /// Static method to register routes for different views within the application.
        /// </summary>
        private static void InitializeRouting()
        {
            // Register routes for various views in the application.
            Routing.RegisterRoute("//login/details", typeof(LoginView));
            Routing.RegisterRoute("//HomePage/details", typeof(CMSHomePage));
            Routing.RegisterRoute("//Recieve/details", typeof(RecieveView));
            Routing.RegisterRoute("//RecieveEdit/details", typeof(RecieveEditView));
            Routing.RegisterRoute("//Dispatch/details", typeof(DispatchView));
            Routing.RegisterRoute("//DispatchEdit/details", typeof(DispatchEditView));
            Routing.RegisterRoute("//Repair/details", typeof(RepairView));
            Routing.RegisterRoute("//RepairEdit/details", typeof(RepairEditView));
            Routing.RegisterRoute("//ReceieveViewData/details", typeof(ReceieveViewData));
            Routing.RegisterRoute("//Settings", typeof(SettingView));
            Routing.RegisterRoute("//Settinge2/details", typeof(SettingPage2));
            Routing.RegisterRoute("//SelectVehicle/details", typeof(SelectVehicleView));
            Routing.RegisterRoute("//SelectContainer/details", typeof(SelectContainerView));
            Routing.RegisterRoute("//InspectContainer/details", typeof(InspectContainerView));

        }

        /// <summary>
        /// Initiates push and pull operations.
        /// </summary>
        public async void PushPull()
        {
            // Check if sync is enabled
            var saveAccessToken = await _secureStorageService.Getsync("savesync");
            if (saveAccessToken != "true") return;



            try
            {
                // Create ContainerInfo object
                ContainerInfo containerInfo = new ContainerInfo()
                {
                    ContainerNumber = "Baboon",
                    Customer = "Jorge"
                };

                // Create RecieveEditViewModel with necessary dependencies
                RecieveEditViewModel recieveEditViewModel = new RecieveEditViewModel(containerInfo, _requestProvider, _navigationService, _context, _secureStorageService);

                // Set the BindingContext
                BindingContext = recieveEditViewModel;

                // Execute the sync command
                recieveEditViewModel.SyncClickCommand.Execute(null);

                // Disable sync after the operation
                await _secureStorageService.savesync("savesync", "false");

                // Verify the change in sync status
                var saveAccessToken2 = await _secureStorageService.Getsync("savesync");


            }
            catch (Exception ex)
            {
               await Shell.Current.DisplayAlert("Error in shell", ex.Message, "Ok");
            }
        }


        /// <summary>
        /// Helper method to retrieve the stored date time.
        /// </summary>
        /// <returns>The current date and time.</returns>
        private DateTime GetStoredDateTime()
        {
            return DateTime.Now;
        }
    }

}