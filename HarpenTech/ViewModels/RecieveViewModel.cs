using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarpenTech.Models.Container;
using HarpenTech.Models.Response;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.ViewModels.Base;
using HarpenTech.Views.RecievePage;
using MvvmHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HarpenTech.ViewModels
{
    // Partial class for the RecieveViewModel, extending ViewModelBase

    public partial class RecieveViewModel : ViewModelBase
    {
        private readonly SelectVehicleView _selectVehicleView;
        private readonly InspectContainerViewModel _inspectContainerViewModel;
        private readonly INavigationService _navigationService;

        // List to store search results
        public static List<ShipInfo> SearchProducts { get; set; } = new();

        // Observable collection to store and bind ShipInfo items
        public ObservableRangeCollection<ShipInfo> Products { get; set; } = new();

        // List to store selected search products
        public static List<ShipInfo> SelectedSearchProducts { get; set; } = new();

        [ObservableProperty]
        string _searchField;
        // ICommand for handling the Edit button click
        public ICommand EditClickCommand { get; }

        #region Properties

        //This property store selected ship from the searchbar
        private ObservableCollection<ContainerInfo> selectedShipContainerItems;
        public ObservableCollection<ContainerInfo> SelectedShipContainerItems
        {
            get => selectedShipContainerItems;
            set => SetProperty(ref selectedShipContainerItems, value);
        }



        // List to store ContainerInfo items
        private readonly IList<ContainerInfo> source;
        private readonly IList<ShipInfo> shipsource;
        public ObservableCollection<string> ShipNameOnly { get; private set; }


        // ObservableCollection to store and bind ContainerInfo items
        public ObservableCollection<ContainerInfo> ContainerItems { get; private set; }
        public ObservableCollection<ShipInfo> ShipInfoItems { get; private set; }

        // Selected ContainerInfo item
        private ContainerInfo selectedContainerItem;

        // Event handler for property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to invoke PropertyChanged event
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion



        #region Constructor

        // Constructor for RecieveViewModel, taking INavigationService as a parameter
        public RecieveViewModel(INavigationService navigationService, InspectContainerViewModel inspectContainerViewModel, SelectVehicleView selectVehicleView) : base(navigationService)
        {
            _navigationService = navigationService;
            _selectVehicleView = selectVehicleView;
            _inspectContainerViewModel = inspectContainerViewModel;

            #region Static List
            List<string> myList = new List<string>();
            myList.Add("Titanic");
            myList.Add("Amster");
            myList.Add("Zyro");
            myList.Add("Blass");
            myList.Add("Damp");
            myList.Add("RandomValue1");
            myList.Add("RandomValue2");
            myList.Add("RandomValue3");
            ShipNameOnly = new ObservableCollection<string>(myList);
            #endregion

            // Initialize the source list
            source = new List<ContainerInfo>();
            shipsource = new List<ShipInfo>();
            ShipInfoItems = new ObservableCollection<ShipInfo>(shipsource);

            // Create and populate the ContainerItems collection
            CreateContainerItemCollection();
            CreateStaticShipSourceCollection();

            //Initialize the list that we get from Tapsearchresults
            SelectedShipContainerItems = new ObservableCollection<ContainerInfo>();

            // Notify about the change in the SelectedContainerItem
            OnPropertyChanged("SelectedContainerItem");
        }
        #endregion



        /// <summary>
        /// Method to create and populate the ContainerItems collection
        /// </summary>
        void CreateContainerItemCollection()
        {
            // Add ContainerInfo items to the source list
            source.Add(new ContainerInfo
            {
                ContainerNumber = "Baboon",
                Customer = "Jorge"

            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Capuchin ContainerItem",
                Customer = "Michel"

            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Blue ContainerItem",
                Customer = "Sam"

            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Squirrel ContainerItem",
                Customer = "Roger"
            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Golden Lion Tamarin",
                Customer = "Marvel"
            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Howler ContainerItem",
                Customer = "Jack"

            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Japanese Macaque",
                Customer = "Rita"
            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Mandrill",
                Customer = "Mark"
            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Proboscis ContainerItem",
                Customer = "Peter"
            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Red-shanked Douc",
                Customer = "Jhon"
            });

            source.Add(new ContainerInfo
            {
                ContainerNumber = "Gray-shanked Douc",
                Customer = "Mia"
            });

            // Create an ObservableCollection with the items from the source list
            ContainerItems = new ObservableCollection<ContainerInfo>(source);
        }


        /// <summary>
        /// Method to create and populate the ShipInfoItems collection
        /// </summary>
        void CreateStaticShipSourceCollection()
        {

            if (SearchProducts.Count < 0) SearchProducts.Clear();
            // Add ContainerInfo items to the source list
            shipsource.Add(new ShipInfo
            {
                ShipId = 1,
                ShipName = "Titanic",
                containerInfos = new List<ContainerInfo> {
                    new ContainerInfo
                    {
                        ContainerNumber = "Baboon",
                        Customer = "Jorge"

                    },
                     new ContainerInfo
                     {
                        ContainerNumber = "Capuchin",
                        Customer = "Sam"

                     },
                      new ContainerInfo
                      {
                        ContainerNumber = "Proboscis",
                        Customer = "Cat"

                      },
                      new ContainerInfo
                      {
                        ContainerNumber = "Mandrill",
                        Customer = "Mark"
                      }
                }
            });

            shipsource.Add(new ShipInfo
            {
                ShipId = 2,
                ShipName = "Imagination",
                containerInfos = new List<ContainerInfo> {
                    new ContainerInfo
                    {
                        ContainerNumber = "Gale",
                        Customer = "Eatson"

                    },
                     new ContainerInfo
                     {
                        ContainerNumber = "Watson",
                        Customer = "Ambani"

                     },
                      new ContainerInfo
                      {
                        ContainerNumber = "Salu",
                        Customer = "Dog"

                      },
                      new ContainerInfo
                      {
                        ContainerNumber = "Cop",
                        Customer = "Atop"
                      }
                }
            });


            shipsource.Add(new ShipInfo
            {
                ShipId = 3,
                ShipName = "Liberty",
                containerInfos = new List<ContainerInfo> {
                    new ContainerInfo
                    {
                        ContainerNumber = "Zephyr",
                        Customer = "Sapphire"

                    },
                     new ContainerInfo
                     {
                        ContainerNumber = "John",
                        Customer = "Walker"

                     },
                      new ContainerInfo
                      {
                        ContainerNumber = "Old",
                        Customer = "Label"

                      },
                      new ContainerInfo
                      {
                        ContainerNumber = "Red",
                        Customer = "Gopsy"
                      }
                }
            });

            shipsource.Add(new ShipInfo
            {
                ShipId = 4,
                ShipName = "Serendipity",
                containerInfos = new List<ContainerInfo> {
                    new ContainerInfo
                    {
                        ContainerNumber = "Propsy",
                        Customer = "Gooo"

                    },
                     new ContainerInfo
                     {
                        ContainerNumber = "Lower",
                        Customer = "Dad"

                     },
                      new ContainerInfo
                      {
                        ContainerNumber = "Alive",
                        Customer = "Sleep"

                      },
                      new ContainerInfo
                      {
                        ContainerNumber = "Theif",
                        Customer = "Chulsy"
                      }
                }
            });
            // Create an ObservableCollection with the items from the source list
            ShipInfoItems = new ObservableCollection<ShipInfo>(shipsource);
            Products.AddRange(shipsource);
            SearchProducts.AddRange(shipsource);

        }


        /// <summary>
        /// To handle search click and call api locally
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task SearchClickRespone()
        {
            try
            {
                #region Api Call Locally

                //RequestProvider requestProvider = new RequestProvider();
                //string api = "http://10.0.2.2:5279/api/Recieve/ReceiveSearch";
                //var responseString = requestProvider.Get<SearchResponse>(api);

                #endregion
               var responseString = 123;
                if (responseString == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "NO DATA AVILABLE", "OK");
                }
                await Shell.Current.Navigation.PushAsync(new SelectVehicleView(_inspectContainerViewModel, _navigationService));

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
                await Shell.Current.Navigation.PushAsync(new SelectVehicleView(_inspectContainerViewModel, _navigationService));

                #endregion

                //await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        /// <summary>
        /// method for handling the Back button click
        /// </summary>
        [RelayCommand]
        public async Task BackClickAsync()
        {
            await NavigationService.NavigateToAsync("//HomePage");
        }

    }
}
