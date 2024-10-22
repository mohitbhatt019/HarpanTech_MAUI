using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using CommunityToolkit.Mvvm.Messaging;
using HarpenTech.Services.Handler;

namespace HarpenTech
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //AutoSync sqlite sync
            //StartService(new Intent(this, typeof(NetworkCheckService)));

            //Change action bar

            WeakReferenceMessenger.Default.Register<FullScreenMessage>(this, (r, m) =>
            {
                IWindowInsetsController wicController = Window.InsetsController;
                Window.SetDecorFitsSystemWindows(false);
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                if (wicController != null)
                {
                    wicController.Hide(WindowInsets.Type.NavigationBars());
                }
            });
            WeakReferenceMessenger.Default.Register<NormalScreenMessage>(this, (r, m) =>
            {
                IWindowInsetsController wicController = Window.InsetsController;
                Window.SetDecorFitsSystemWindows(true);
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
                if (wicController != null)
                {
                    wicController.Show(WindowInsets.Type.NavigationBars());
                }
            });
        }

        public override Resources Resources
        {

            get
            {

                Resources resource = base.Resources;
                Configuration configuration = new Configuration();
                configuration.SetTo(configuration);
                //configuration.SetToDefaults();
                /*    if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.NMr1)
                    {
                        return CreateConfigurationContext(configuration).Resources;
                    }*/
                return resource;
            }
        }

        /* public override Resources Resources
         {

             get
             {

                 Resources resource = base.Resources;
                 Configuration configuration = new Configuration();
                 configuration.SetTo(configuration);
                 //configuration.SetToDefaults();
                 if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.NMr1)
                 {
                     resource.UpdateConfiguration(configuration, resource.DisplayMetrics);
                     return resource;
                 }
                 return resource;
             }
         }*/


        protected override void OnDestroy()
        {
            base.OnDestroy();

            //AutoSync sqlite sync
            // Stop the background service when the app is destroyed
            //StopService(new Intent(this, typeof(NetworkCheckService)));
        }
    }
}
