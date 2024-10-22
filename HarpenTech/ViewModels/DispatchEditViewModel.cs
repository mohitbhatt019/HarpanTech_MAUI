using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models.Dispatch;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.ViewModels.Base;
using System.Collections.ObjectModel;

namespace HarpenTech.ViewModels
{
    // Partial class for the DispatchEditViewModel, extending ViewModelBase
    public partial class DispatchEditViewModel : ViewModelBase
    {
        private readonly DatabaseContext _context;

        // Constructor for DispatchEditViewModel, taking INavigationService as a parameter
        public DispatchEditViewModel(INavigationService navigationService, DatabaseContext context) : base(navigationService)
        {
            _context = context;
            LoadProductsAsync();
        }

        #region Observable Properties

        // Observable property for the customer name
        [ObservableProperty]
        private string _customer;

        // Observable property for the booking reference
        [ObservableProperty]
        private string _bookingReference;

        // Observable property for the container number
        [ObservableProperty]
        private int _containerNumber;

        // Observable property for the ISO
        [ObservableProperty]
        private int _iso;

        // Observable property for the temperature instruction
        [ObservableProperty]
        private string _tempInstruction;

        // Observable property for the first seal
        [ObservableProperty]
        private string _seal1;

        // Observable property for the second seal
        [ObservableProperty]
        private string _seal2;

        // Observable property for the temperature in degrees Celsius
        [ObservableProperty]
        private string _tempInDegreeCelcius;

        // Observable property for the DispatchEdit model
        [ObservableProperty]
        private DispatchEdit _dispatchEditModel;

        // Observable property for the collection of DispatchModel instances
        [ObservableProperty]
        public ObservableCollection<DispatchModel> _products = new();

        // Observable property representing the currently selected DispatchModel for operations
        [ObservableProperty]
        private DispatchModel _operatingProduct = new();

        // Observable property indicating whether the ViewModel is currently performing a task
        [ObservableProperty]
        private bool _isBusy;

        // Observable property for displaying a message or description of the current busy task
        [ObservableProperty]
        private string _busyText;

        #endregion



        /// <summary>
        /// RelayCommand method for handling the cancel button click
        /// </summary>
        [RelayCommand]
        private async Task CancelClickAsync()
        {
            // Navigate back asynchronously
            await Shell.Current.Navigation.PopAsync();
        }

        /// <summary>
        /// RelayCommand method for handling the save button click
        /// </summary>
        [RelayCommand]
        private async Task SaveClickAsync()
        {

            #region Sqlite Db
            DispatchModel dispatchModel = new DispatchModel()
            {
                Customer = _customer,
                BookingReference = _bookingReference,
                ContainerNo = _containerNumber,
                ISO = _iso,
                TempInstruction = _tempInstruction,
                Temp = TempInDegreeCelcius,
                Seal1 = Seal1,
                Seal2 = Seal2
            };
            #endregion


        }

        #region Database Work
        /// <summary>
        /// Asynchronously loads products from the database and populates the Products collection.
        /// </summary>
        public async Task LoadProductsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var products = await _context.GetAllAsync<DispatchModel>();
                if (products is not null && products.Any())
                {
                    Products ??= new ObservableCollection<DispatchModel>();

                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }
            }, "Fetching products...");

        }


        /// <summary>
        /// Sets the currently operating product based on user selection.
        /// </summary>
        [RelayCommand]
        private void SetOperatingProduct(DispatchModel? product) => OperatingProduct = product ?? new();

        /// <summary>
        /// Asynchronously saves or updates the currently operating product in the database.
        /// </summary>
        [RelayCommand]
        private async Task SaveProductAsync()
        {
            #region Mapping_OperatingProduct
            OperatingProduct.Customer = _customer;
            OperatingProduct.BookingReference = _bookingReference;
            OperatingProduct.ContainerNo = _containerNumber;
            OperatingProduct.ISO = _iso;
            OperatingProduct.TempInstruction = _tempInstruction;
            OperatingProduct.Temp = _tempInDegreeCelcius;
            OperatingProduct.Seal1 = _seal1;
            OperatingProduct.Seal2 = _seal2;
            #endregion

            if (OperatingProduct is null)
                return;

            var (isValid, errorMessage) = OperatingProduct.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                return;
            }

            var busyText = OperatingProduct.Id == 0 ? "Creating product..." : "Updating product...";
            await ExecuteAsync(async () =>
            {
                if (OperatingProduct.Id == 0)
                {
                    // Create product
                    await _context.AddItemAsync<DispatchModel>(OperatingProduct);
                    Products.Add(OperatingProduct);
                    await Shell.Current.DisplayAlert("Alert", "Data saved Successfully", "Ok");

                    Customer = string.Empty;
                    BookingReference = string.Empty;
                    ContainerNumber = 0;
                    Iso = 0;
                    TempInstruction = string.Empty;
                    TempInDegreeCelcius = string.Empty;
                    Seal1 = string.Empty;
                    Seal2 = string.Empty;


                }
                else
                {
                    // Update product
                    if (await _context.UpdateItemAsync<DispatchModel>(OperatingProduct))
                    {
                        var productCopy = OperatingProduct.Clone();

                        var index = Products.IndexOf(OperatingProduct);
                        Products.RemoveAt(index);

                        Products.Insert(index, productCopy);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Product updation error", "Ok");
                        return;
                    }
                }
                SetOperatingProductCommand.Execute(new());
            }, busyText);
        }

        /// <summary>
        /// Asynchronously deletes a product with the specified id from the database.
        /// </summary>
        [RelayCommand]
        private async Task DeleteProductAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<DispatchModel>(id))
                {
                    var product = Products.FirstOrDefault(p => p.Id == id);
                    Products.Remove(product);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Product was not deleted", "Ok");
                }
            }, "Deleting product...");
        }

        /// <summary>
        /// Executes an asynchronous operation while managing the IsBusy state.
        /// </summary>
        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }



        #endregion

    }
}