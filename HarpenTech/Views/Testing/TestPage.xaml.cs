using HarpenTech.Models.Container;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using HarpenTech.Views.datascreen;

namespace HarpenTech.Views.Testing;

public partial class TestPage : ContentPage
{
    private readonly IRequestProvider _requestProvider;

    private readonly DatabaseContext _context;

    private readonly ContainerInfo _containerItem;
    private readonly ISecureStorageService _secureStorage;


    // ViewModel associated with the view
    private readonly RecieveEditViewModel _viewModel; 
    public TestPage(IRequestProvider requestProvider, ContainerInfo containerItem, RecieveEditViewModel viewModel, DatabaseContext context, ISecureStorageService secureStorage)
	{
        _secureStorage = secureStorage;
        _requestProvider = requestProvider;
        _context = context;
        _containerItem = containerItem;
        InitializeComponent();
	 	NavigateToDataPage();

    }

    // Method for navigating to the data page by popping the current page from the navigation stack
    public async Task NavigateToDataPage()
	{
       await Shell.Current.Navigation.PopAsync();

    }
}