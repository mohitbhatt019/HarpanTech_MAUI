using HarpenTech.Models.Settings;
using HarpenTech.NewFolder;
using HarpenTech.Services;

namespace HarpenTech.Views.ExternalScreen;

public partial class BlankPage : ContentPage
{

    private readonly INavigationService _navigationService;

    public BlankPage(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    // This method is called when the Page is about to appear on the screen.
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        DatabaseContext _context = new DatabaseContext();
        IEnumerable<CmsSetting> products = await _context.GetAllAsync<CmsSetting>();
        if (products.Count() == 0)
        {
            await _navigationService.NavigateToAsync("//Settings");
        }
       
    }

}