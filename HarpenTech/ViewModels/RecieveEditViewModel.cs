using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models.Container;
using HarpenTech.Models.Recieve;
using HarpenTech.Models.Response;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels.Base;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;



namespace HarpenTech.ViewModels
{
    // Partial class for the RecieveEditViewModel, extending ViewModelBase
    public partial class RecieveEditViewModel : ViewModelBase
    {
        private readonly ISecureStorageService _secureStorageService;

        private readonly DatabaseContext _context;
        private readonly IRequestProvider requestProvider;

        // Private field to handle HTTP requests
        private readonly IRequestProvider _requestProvider;

        #region Observable Properties
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;
        // Observable property for the container number
        [ObservableProperty]
        private string _containerNumber;

        // Observable property for the registration number
        [ObservableProperty]
        private string _regNo;

        // Observable property for the transporter information
        [ObservableProperty]
        private string _transporter;

        // Observable property for remarks
        [ObservableProperty]
        private string _remarks;

        // Observable property for ISO information
        [ObservableProperty]
        private string _iso;

        // Observable property for the status
        [ObservableProperty]
        private string _status;

        // Observable property for grading information
        [ObservableProperty]
        private string _grading;

        // Observable property for the image data
        [ObservableProperty]
        private string _image;

        // Observable property for the customer information
        [ObservableProperty]
        private string _customer;

        // Observable property for damage information
        [ObservableProperty]
        private string _damage;

        //Observable property for id of container Reciever
        [ObservableProperty]
        private int _containerRecieverId;

        // Observable property for the title 
        [ObservableProperty]
        private string _title;

        // Observable property indicating whether the container is marked as complete or not
        [ObservableProperty]
        private bool _isComplete;

        // Observable property for the unique identifier of the container
        [ObservableProperty]
        private string _id;

        [ObservableProperty]
        private bool _deleted;

        // Observable property for the date and time when the container was last updated
        [ObservableProperty]
        private DateTimeOffset _updatedAt;

        // Observable property for the last updated date and time in string format
        [ObservableProperty]
        private string _updatedAtInString;

        // Navigation service used for handling navigation between pages
        INavigationService _navigationService;

        // Observable collection for products
        [ObservableProperty]
        public ObservableCollection<ContainerReciever> _products = new();

        // Operating product for CRUD operations
        [ObservableProperty]
        private ContainerReciever _operatingProduct = new();



        #endregion



        /// <summary>
        /// Constructor for RecieveEditViewModel, taking ContainerInfo and INavigationService as parameters
        /// </summary>
        /// <param name="containerItem">Container information.</param>
        /// <param name="requestProvider">Request provider for HTTP requests.</param>
        /// <param name="navigationService">Navigation service for handling navigation between pages.</param>
        /// <param name="context">Database context.</param>
        /// <param name="secureStorageService">Secure storage service for handling secure storage.</param>
        public RecieveEditViewModel(ContainerInfo containerItem, IRequestProvider requestProvider, INavigationService navigationService, DatabaseContext context, ISecureStorageService secureStorageService) : base(navigationService)
        {
            _requestProvider = requestProvider;
            _requestProvider.SetBaseAddress("http://10.0.2.2:5279/api/");
            // Initialize observable properties with values from the provided ContainerInfo
            _containerNumber = containerItem.ContainerNumber;
            _customer = containerItem.Customer;
            _context = context;
            _secureStorageService = secureStorageService;
            LoadProductsAsync();
        }

        #region Database Work
        /// <summary>
        /// Loads products asynchronously from the database.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task LoadProductsAsync()
        {
            await ExecuteAsync(async () =>
            {
                await Task.Delay(2000);

                var products = await _context.GetAllAsync<ContainerReciever>();
                if (products is not null && products.Any())
                {
                    Products ??= new ObservableCollection<ContainerReciever>();

                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }

            }, "Fetching products...");

        }

        /// <summary>
        /// Sets the operating product for CRUD operations.
        /// </summary>
        /// <param name="product">The product to set as the operating product.</param>
        [RelayCommand]
        private void SetOperatingProduct(ContainerReciever product) => OperatingProduct = product ?? new();

        /// <summary>
        /// Saves the product asynchronously, either creating or updating based on the product's ID.
        /// </summary>
        [RelayCommand]
        public async void SaveProductAsync()
        {
            var busyText = OperatingProduct.ContainerRecieverId == 0 ? "Creating product..." : "Updating product...";

            await ExecuteAsync(async () =>
            {

                #region Mapping
                OperatingProduct.Customer = Customer;
                OperatingProduct.ContainerNo = ContainerNumber;
                OperatingProduct.Remarks = Remarks;
                OperatingProduct.Title = "Add";
                OperatingProduct.IsComplete = true;
                OperatingProduct.Version = "";
                OperatingProduct.Deleted = false;
                OperatingProduct.UpdatedAt = DateTimeOffset.UtcNow;
                OperatingProduct.UpdatedAtInString = DateTimeOffset.UtcNow.ToString();
                OperatingProduct.Image = Image;
                OperatingProduct.Id = Guid.NewGuid().ToString();
                #endregion
                // Check if any of the required properties is null
                List<string> emptyFields = new List<string>();

                if (OperatingProduct.Image == null) emptyFields.Add("Image");

                if (emptyFields.Any())
                {
                    string emptyFieldsMessage = $"Please Click the {string.Join(", ", emptyFields)} before saving:\n";
                    await Shell.Current.DisplayAlert("Alert", emptyFieldsMessage, "Ok");
                    return;
                }

                var (isValid, errorMessage) = OperatingProduct.Validate();
                if (!isValid)
                {
                    await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                    return;
                }
                var busyText = OperatingProduct.ContainerRecieverId == 0 ? "Creating product..." : "Updating product...";

                if (OperatingProduct.Image != null && OperatingProduct.Customer != null && OperatingProduct.Remarks != null)
                {
                    // Create product
                    await _context.AddItemAsync<ContainerReciever>(OperatingProduct);
                    Products.Add(OperatingProduct);

                    #region Toster
                    string text = "Data saved Successfully";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 20;

                    var toast = Toast.Make(text, duration, fontSize);

                    await toast.Show();
                    #endregion

                    Remarks = string.Empty;
                    Image = string.Empty;
                    Customer = string.Empty;
                    ContainerNumber = string.Empty;

                }
                else
                {
                    // Update product
                    if (await _context.UpdateItemAsync<ContainerReciever>(OperatingProduct))
                    {
                        var productCopy = OperatingProduct.Clone();

                        var index = Products.IndexOf(OperatingProduct);
                        Products.RemoveAt(index);

                        Products.Insert(index, productCopy);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Please fill all the fields", "Ok");
                        return;
                    }
                }
                SetOperatingProductCommand.Execute(new());
                await Task.Delay(2000);

            });




        }

        /// <summary>
        /// Deletes the product asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [RelayCommand]
        private async Task DeleteProductAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<ContainerReciever>(id))
                {
                    var product = Products.FirstOrDefault(p => p.ContainerRecieverId == id);
                    Products.Remove(product);
                    await Task.Delay(2000);


                    #region Toster
                    string text2 = "Record Deleted Successfully";
                    ToastDuration duration2 = ToastDuration.Short;
                    double fontSize2 = 17;

                    var toast2 = Toast.Make(text2, duration2, fontSize2);

                    await toast2.Show();
                    #endregion

                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Product was not deleted", "Ok");
                }
            }, "Deleting product...");
        }

        /// <summary>
        /// Executes an asynchronous operation with error handling and busy indicators.
        /// </summary>
        /// <param name="operation">The asynchronous operation to execute.</param>
        /// <param name="busyText">The text to display during the operation.</param>
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
        /// <summary>
        /// Initiates the synchronization process for data.
        /// </summary>
        [RelayCommand]
        private async Task SyncClickAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                try
                {
                    await PullItemsAsync();
                    await PushItemsAsync();

                }

                catch (Exception ex)
                {
                    #region Toster
                    string text2 = "Something went wrong while syncing";
                    ToastDuration duration2 = ToastDuration.Short;
                    double fontSize2 = 20;

                    var toast2 = Toast.Make(text2, duration2, fontSize2);

                    await toast2.Show();
                    #endregion
                }
                #region Toster Alert
                string text = "Syncing Completed Successfully";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show();
                #endregion
            }
        }

        /// <summary>
        /// Retrieves and returns a list of updated records from the database based on the last update date.
        /// </summary>
        /// <returns>List of updated records.</returns>
        public async Task<IEnumerable<ContainerReciever>> GetUpdatedRecordsAsync()
        {
            try
            {

                var dateString = await _secureStorageService.GetLastUpdateDateTimeOffSet("LastUpdateDateTimeOffSet");

                if (DateTimeOffset.TryParse(dateString, out DateTimeOffset date))
                {
                    // Filter the products list to get records with UpdateAt greater than or equal to the given DateTime
                    var products = await _context.GetAllAsync<ContainerReciever>();

                    var updatedRecords = products
                        .Where(product => DateTimeOffset.TryParse(product.UpdatedAtInString, out DateTimeOffset productDate) && productDate > date)
                        .ToList();

                    if (updatedRecords.Count > 0)
                    {
                        return updatedRecords;
                    }
                }
                else
                {
                    #region Toster Alert
                    string text = "SecureStorage DateTime Not Available";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show();
                    #endregion
                }

            }
            catch (Exception ex)
            {

                #region Toster Alert
                string text = "Something went wrong while GetUpdatedRecordsAsync";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show();
                #endregion
            }

            return null;
        }

        /// <summary>
        /// Pushes new or updated records to a remote API for synchronization.
        /// </summary>
        /// <returns></returns>
        public async Task PushItemsAsync()
        {
            try
            {
                if (Products.Count <= 0)
                {
                    #region Toster Alert
                    string text = "Sync Completed Successfully But No New Record Found";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show();
                    #endregion
                }

                var duplicaterecord = await GetUpdatedRecordsAsync();
                if (duplicaterecord != null)
                {
                    foreach (var item in duplicaterecord.ToList())
                    {
                        var response = await _requestProvider.PostAsync<List<ContainerReciever>, ContainerReciever>("Recieve/AddRecieveTestFormData", item);
                    }
                }
                else
                {
                    #region Toster Alert
                    string text = "Sync Completed Successfully But No New Record Found";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region Toster Alert
                string text = "Something Got Wrong While PushItemsAsync";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show();
                #endregion
            }

        }

        /// <summary>
        /// Pulls records from a remote API and updates the local database.
        /// </summary>
        public async Task PullItemsAsync()
        {
            try
            {
                var sqlite = await _context.GetAllAsync<ContainerReciever>();
                List<string> dateTimeListSqlites = new List<string>();

                foreach (var item in sqlite.ToList())
                {
                    dateTimeListSqlites.Add(item.UpdatedAtInString);
                }


                string api = "http://10.0.2.2:5279/api/Recieve/GetRecieveTestFormData";
                RecieveModel datas = new RecieveModel();
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(api);

                var responseBody = await response.Content.ReadAsStringAsync();

                var tokenData = JsonConvert.DeserializeObject<List<ReceiveItemResponse>>(responseBody);

                // Read the Last-Save-Date-Time header
                if (response.Headers.TryGetValues("Last-Save-Date-Time", out var lastSaveDateTimeHeaderValues))
                {
                    var lastSaveDateTimeHeaderValue = lastSaveDateTimeHeaderValues.FirstOrDefault();

                    // Parse the header value as needed
                    if (DateTimeOffset.TryParseExact(lastSaveDateTimeHeaderValue, "yyyy-MM-ddTHH:mm:ss.fffffffK", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var lastSaveDateTime))
                    {
                        // Use the lastSaveDateTime as needed
                        await _secureStorageService.SaveLastUpdateDateTimeOffSet(lastSaveDateTime);
                    }
                }

                foreach (var item in tokenData.ToList())
                {
                    if (item == null) return;

                    if (dateTimeListSqlites.Contains(item.UpdatedAt.ToString()))
                    {
                        // Skip adding to SQLite as it already exists
                        continue;
                    }
                    ContainerReciever containerReciever = new ContainerReciever()
                    {
                        Title = item.Title,
                        IsComplete = item.IsComplete,
                        Id = item.Id.ToString(),
                        ContainerNo = item.ContainerNo,
                        Customer = item.Customer,
                        Remarks = item.Remarks,
                        UpdatedAt = item.UpdatedAt,
                        Version = item.Version,
                        Deleted = item.Deleted,
                        UpdatedAtInString = item.UpdatedAt.ToString(),
                        Image = item.Image,
                    };
                    var saveInSqlite = await _context.AddItemAsync<ContainerReciever>(containerReciever);
                }
            }
            catch (Exception ex)
            {
                #region Toster Alert
                string text = "Something Got Wrong While Pull";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show();
                #endregion
            }
        }

        /// <summary>
        /// RelayCommand method for handling the cancel button click. Navigates back asynchronously.
        /// </summary>
        [RelayCommand]
        private async Task CancelClickAsync()
        {
            // Navigate back asynchronously
            await Shell.Current.Navigation.PopAsync();
        }

        /// <summary>
        /// RelayCommand method for handling the camera button click
        /// </summary>
        /// <returns></returns>
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
        /// Asynchronously loads a photo into the 'Image' property.
        /// </summary>
        /// <param name="photo">The FileResult containing the photo data.</param>
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
        /// Converts the given FileResult containing photo data to a base64 string.
        /// </summary>
        /// <param name="photo">The FileResult containing the photo data.</param>
        /// <returns>A base64 string representation of the photo.</returns>
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

        /// <summary>
        /// RelayCommand method for handling the save button click
        /// </summary>
        [RelayCommand]
        private async Task SaveClickAsync()
        {
            // Create a RecieveModel with data from observable properties
            RecieveModel recieveModel = new RecieveModel()
            {
                ContainerNo = ContainerNumber,
                Customer = Customer,
                Remarks = Remarks
            };
        }

    }
}
