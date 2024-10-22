using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Util;
using HarpenTech.Models.Container;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;


namespace HarpenTech.Services.PartialMethods
{
    [Service]
    public partial class NetworkCheckService : Service
    {

        private bool isChecking = false;
        private Android.OS.Handler handler;
        private readonly ISecureStorageService _secureStorageService;
        private readonly INavigationService _navigationService;

        private readonly DatabaseContext _context;
        private readonly IRequestProvider _requestProvider;
        public readonly RecieveEditViewModel _viewModel;
        private readonly SettingViewModel _settingViewModel;

        public NetworkCheckService(ContainerInfo containerItem, IRequestProvider requestProvider, INavigationService navigationService, DatabaseContext context, ISecureStorageService secureStorageService, SettingViewModel settingViewModel)
        {
            _requestProvider = requestProvider;
            _context = context;
            _secureStorageService = secureStorageService;
            _viewModel = new RecieveEditViewModel(containerItem, requestProvider, navigationService, context, secureStorageService);
            _settingViewModel = settingViewModel;
            
        }





        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            isChecking = true;
            handler = new Android.OS.Handler();
            handler.PostDelayed(new Java.Lang.Runnable(() => CheckNetworkAccess()), (long)TimeSpan.FromSeconds(2).TotalMilliseconds);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isChecking = false;
        }

        private async Task CheckNetworkAccess()
        {
            if (isChecking)
            {
                // Use ConnectivityManager to check network access
                ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
                NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;

                // if network is available
                bool isNetworkAvailable = networkInfo != null && networkInfo.IsConnected;

                try
                {
                    if (isNetworkAvailable)
                    {
                        try
                        {

                            InvokeMethod(_navigationService, _secureStorageService, _requestProvider, _context, _settingViewModel);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error");
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                    Log.Error("NetworkCheckService", $"Exception in CheckNetworkAccess: {ex.Message}");

                }

                // Schedule the next check after a delay
                handler.PostDelayed(new Java.Lang.Runnable(() => CheckNetworkAccess()), (long)TimeSpan.FromMinutes(2).TotalMilliseconds);
            }
        }
       



    }
}


    




