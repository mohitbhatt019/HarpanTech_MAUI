using HarpenTech.Models.Container;
using HarpenTech.NewFolder;
using HarpenTech.Services.RequestProvider;
using HarpenTech.Services.SecureServiceStorage;
using HarpenTech.ViewModels;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HarpenTech.Views.datascreen;

public partial class ReceieveViewData : ContentPageBase
{
    private readonly ISecureStorageService _secureStorage;
    private readonly DatabaseContext _context;

    private readonly IRequestProvider _requestProvider;

    private readonly ContainerInfo _containerItem;

    public ReceieveViewData(DatabaseContext context, ContainerInfo containerItem, IRequestProvider requestProvider, ISecureStorageService secureStorage)
    {
        _secureStorage = secureStorage;
        _requestProvider = requestProvider;
        _context = context;
        _containerItem = containerItem;
        BindingContext = new RecieveEditViewModel(containerItem, _requestProvider, null, context, _secureStorage);
        InitializeComponent();
    }

    // Event handler for the delete button click.
    private async void DeleteClick(object sender, EventArgs e)
    {
        bool result = await App.Current.MainPage.DisplayAlert(
                        "Alert",
                        "You sure want to delete?",
                        "Yes",
                        "Cancel");

        // If user chooses to quit, exit the app
        if (result)
        {
            ((RecieveEditViewModel)BindingContext).DeleteProductCommand.Execute(e);
        }
    }

    // Event handler for checking and displaying the last update date.
    private async void TimeCheck(object sender, EventArgs e)
    {
        try
        {
            var date = await _secureStorage.GetLastUpdateDateTimeOffSet("LastUpdateDateTimeOffSet");
            if (!string.IsNullOrEmpty(date)) await Shell.Current.DisplayAlert("Alert", date.ToString(), "Ok");
            else await Shell.Current.DisplayAlert("Alert", "Secure Storage is empty, Take Pull First", "Ok");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert in TimeCheck", ex.Message, "Ok");
        }
    }
}
