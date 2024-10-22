using CommunityToolkit.Mvvm.Input;
using HarpenTech.Services;
using HarpenTech.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.ViewModels
{
    // Partial class for the RepairViewModel, extending ViewModelBase
    public partial class RepairViewModel : ViewModelBase
    {
        // Constructor for RepairViewModel, taking INavigationService as a parameter
        public RepairViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        /// <summary>
        /// RelayCommand method for handling the RepairEdit button click asynchronously
        /// </summary>
        /// <returns>A Task representing the asynchronous operation</returns>
        [RelayCommand]
        public async Task RepairEditClickAsync()
        {
            // Navigate to the RepairEdit page
            await NavigationService.NavigateToAsync("//RepairEdit/details");
        }

        /// <summary>
        /// RelayCommand method for handling the Back button click asynchronously
        /// </summary>
        /// <returns>A Task representing the asynchronous operation</returns>
        [RelayCommand]
        public async Task BackClickAsync()
        {
            await NavigationService.NavigateToAsync("//HomePage");

        }

    }

}
