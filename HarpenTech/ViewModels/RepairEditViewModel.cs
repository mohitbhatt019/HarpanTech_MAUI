using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models.Repair;
using HarpenTech.Services;
using HarpenTech.ViewModels.Base;

namespace HarpenTech.ViewModels
{
    // Partial class for the RepairEditViewModel, extending ViewModelBase
    public partial class RepairEditViewModel : ViewModelBase
    {
        #region Observable properties
        // Observable properties for various repair options
        [ObservableProperty]
        private bool _repairNeeded;

        [ObservableProperty]
        private bool _major;

        [ObservableProperty]
        private bool _wash;

        [ObservableProperty]
        private bool _wasteMaterial;

        [ObservableProperty]
        private bool _gasket;

        [ObservableProperty]
        private bool _corrosionPinhole;

        [ObservableProperty]
        private bool _cut;

        [ObservableProperty]
        private bool _dent;

        [ObservableProperty]
        private bool _bentLocking;

        [ObservableProperty]
        private bool _floorLoose;

        [ObservableProperty]
        private bool _floorDamageFloorDelam;

        [ObservableProperty]
        private bool _underStructureFloor;
        #endregion

        // Constructor for RepairEditViewModel, taking INavigationService as a parameter
        public RepairEditViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        /// <summary>
        /// RelayCommand method for handling the camera button click
        /// </summary>
        [RelayCommand]
        private async Task CameraClickAsync()
        {
            try
            {
                // Capture a photo using the default MediaPicker
                FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();

                // Display a success alert if the photo is captured
                if (myPhoto != null)
                    await Application.Current.MainPage.DisplayAlert("Success", "Photo saved in Photos", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Alert", "OK", "OK");
            }
            catch (Exception ex)
            {
                // Display an error alert if an exception occurs
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// RelayCommand method for handling the save button click
        /// </summary>
        [RelayCommand]
        private async Task SaveClickAsync()
        {
            // Create a RepairModel with data from observable properties
            RepairModel repairModel = new RepairModel()
            {
                RepairNeeded = RepairNeeded,
                Major = Major,
                Wash = Wash,
                WasteMaterial = WasteMaterial,
                Gasket = Gasket,
                CorrosionPinhole = CorrosionPinhole,
                Cut = Cut,
                Dent = Dent,
                BentLocking = BentLocking,
                FloorLoose = FloorLoose,
                FloorDamageFloorDelam = FloorDamageFloorDelam,
                UnderStructureFloor = UnderStructureFloor
            };

        }
    }
}
