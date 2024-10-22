using CommunityToolkit.Maui.Core.Platform;
using HarpenTech.Models.Container;
using HarpenTech.ViewModels;
using HarpenTech.Views.RecievePage;
using Microsoft.Maui.Handlers;
//using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui.Platform;

namespace HarpenTech.Services.Handler
{

    // Custom SearchHandler for product search functionality
    public class ProductSearchHandler : SearchHandler
    {
        // Injected navigation service for handling navigation operations
        private INavigationService _navigationService;

        // ViewModel associated with the InspectContainerView
        private readonly InspectContainerViewModel _viewModel;

        // List of ShipInfo objects representing products
        public IList<ShipInfo> Products { get; set; }

        // List of selected ContainerInfo objects
        public IList<ContainerInfo> SelectedShipList { get; set; }

        // Navigation route string
        public string NavigationRoute { get; set; }

        // Method invoked when the search query changes
        protected async override void OnQueryChanged(string oldValue, string newValue)
        {
            // Call the base class implementation
            base.OnQueryChanged(oldValue, newValue);

            // Set the ItemsSource to null if the query is empty
            if (string.IsNullOrEmpty(oldValue) || string.IsNullOrEmpty(newValue))
                ItemsSource = null;
            else
                ItemsSource = Products.Where(t => t.ShipName.ToLower().Contains(newValue.ToLower())).ToList();

            // Get a filtered list based on the search query
            List<ShipInfo> list = Products.Where(t => t.ShipName.ToLower().Contains(newValue.ToLower())).ToList();

            // If there is a single match, navigate to the SelectVehicleView
            if (list.Count == 1)
            {
                foreach (var item in list)
                {
                    var viewModel = (RecieveViewModel)BindingContext;
                    InspectContainerViewModel inspectContainerViewModel = new InspectContainerViewModel(_navigationService);
                    await Shell.Current.Navigation.PushAsync(new SelectVehicleView(inspectContainerViewModel, _navigationService));
                }
            }
        }

        // Method invoked when an item is selected
        protected async override void OnItemSelected(object item)
        {
            // If the selected item is a ShipInfo object
            if (item is ShipInfo selectedShip)
            {
                // Get the ViewModel associated with the RecieveView
                var viewModel = (RecieveViewModel)BindingContext;

                // Create an instance of InspectContainerViewModel
                InspectContainerViewModel inspectContainerViewModel = new InspectContainerViewModel(_navigationService);

                // Navigate to the SelectVehicleView
                await Shell.Current.Navigation.PushAsync(new SelectVehicleView(inspectContainerViewModel, _navigationService));
            }

            // Call the base class implementation
            base.OnItemSelected(item);
        }
    }

}
