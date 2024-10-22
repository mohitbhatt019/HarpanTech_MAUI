using HarpenTech.Models.Container;
using HarpenTech.Models.LoginView;
using HarpenTech.NewFolder;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HarpenTech.Services.PartialMethods
{

    public partial class NetworkCheckService
    {
        partial void InvokeMethod();

        //Default Constructor
        public NetworkCheckService()
        {
            
        }

        // Method to invoke various services and initialize the application
        public async void InvokeMethod(INavigationService navigationService, ISecureStorageService secureStorageService, IRequestProvider requestProvider,  DatabaseContext context,SettingViewModel _settingviewModel)
        {
            #region Services
            MauiNavigationService mauiNavigationService = new MauiNavigationService(null);
            SecureStorageService secureStorageService1 = new SecureStorageService();
            RequestProvider.RequestProvider requestProvider1 = new RequestProvider.RequestProvider();
            DatabaseContext databaseContext1 = new DatabaseContext();
            SecureStorageService secureStorageService2 = new SecureStorageService();
            SettingViewModel settingViewModel = new SettingViewModel(navigationService,context, requestProvider1);
            #endregion

            await secureStorageService1.savesync("savesync", "true");
            InitializeAppShell2(mauiNavigationService, secureStorageService2, requestProvider1, databaseContext1, settingViewModel);
        }


        // Method to initialize the application shell
        private void InitializeAppShell2(INavigationService navigationService, ISecureStorageService secureStorageService, IRequestProvider requestProvider, DatabaseContext context, SettingViewModel _settingviewModel)
        {
            AppShell appShell = new AppShell(navigationService, secureStorageService, requestProvider, context,_settingviewModel);
           
        }

    }
}
