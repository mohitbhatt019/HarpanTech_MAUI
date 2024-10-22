using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models.Response;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.ViewModels.Base;
using HarpenTech.Views.RecievePage;
using System.Collections.ObjectModel;

namespace HarpenTech.ViewModels
{
    public partial class InspectContainerViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        #region Observable Properties

        // Observable property for container image
        [ObservableProperty]
        private string _image;

        // Observable property for container status
        [ObservableProperty]
        private string _status;

        // Observable property for container grade
        [ObservableProperty]
        private string _grade;

        // Observable property for container damage
        [ObservableProperty]
        private string _damage;

        #endregion

        #region Static List 

        // List of possible damage options
        public ObservableCollection<string> DamageList { get; set; }

        // List of possible status options
        public ObservableCollection<string> StatusList { get; set; }

        // List of possible grade options
        public ObservableCollection<string> GradeList { get; set; }

        #endregion

        // Constructor for the ViewModel
        public InspectContainerViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;

            // Initializing the DamageList with predefined options
            DamageList = new ObservableCollection<string>
        {
            "Yes",
            "No",
            "Not sure"
        };

            // Initializing the StatusList with predefined options
            StatusList = new ObservableCollection<string>
        {
            "Approved",
            "Pending",
            "Reject"
        };

            // Initializing the GradeList with predefined options
            GradeList = new ObservableCollection<string>
        {
            "G-A",
            "G-B",
            "G-C"
        };
        }



        /// <summary>
        /// Asynchronous method for handling the camera button click event.
        /// Captures a photo and displays a Toast notification.
        /// </summary>
        [RelayCommand]
        private async Task CameraClickAsync()
        {
            try
            {
                // Capture a photo using the default MediaPicker
                FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
                await LoadPhotoAsync(myPhoto);

                // Display a success alert if the photo is captured
                if (myPhoto != null)
                {
                    #region Toster Alert
                    string text = "Photo saved in Gallary";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show();
                    #endregion
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Alert", "OK", "OK");
            }
            catch (Exception ex)
            {
                #region Toster Alert
                string text = "Something Got Wrong While CameraClickAsync";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show();
                #endregion
            }
        }


        /// <summary>
        /// Asynchronous method for loading a photo and converting it to a base64 string.
        /// </summary>
        public async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                Image = null;
                return;
            }

            try
            {
                Image = await ToBase64String(photo);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }


        /// <summary>
        /// Converts a photo to a base64 string.
        /// </summary>
        public async Task<string> ToBase64String(FileResult photo)
        {
            try
            {
                using (Stream stream = await photo.OpenReadAsync())

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    byte[] byteImage = memoryStream.ToArray();
                    return Convert.ToBase64String(byteImage);
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            return null;
        }

        //To backend on viewtapped click call api
        /// <summary>
        /// Asynchronous method to get the Container response from backend
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task ContainerClickRespone()
        {
            try
            {
                #region Local Api Call

                //RequestProvider requestProvider = new RequestProvider();
                //string api = "http://10.0.2.2:5279/api/Recieve/ContainerSearch";
                //var responseString = requestProvider.Get<ContainerResponse>(api);

                #endregion

                InspectContainerViewModel inspectContainerViewModel = new InspectContainerViewModel(_navigationService);
                await Shell.Current.Navigation.PushAsync(new SelectContainerView(inspectContainerViewModel, _navigationService));
            }
            catch (Exception ex)
            {
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

                string text = " \u2713 Internal Api Call Sucessfully";
                string actionButtonText = "";
                Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(4);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await Task.Delay(1000);
                await snackbar.Show();
                InspectContainerViewModel inspectContainerViewModel = new InspectContainerViewModel(_navigationService);
                await Shell.Current.Navigation.PushAsync(new SelectContainerView(inspectContainerViewModel, _navigationService));

                #endregion

                //await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
