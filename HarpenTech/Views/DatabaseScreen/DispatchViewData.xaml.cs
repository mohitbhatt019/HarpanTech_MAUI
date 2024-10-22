using HarpenTech.NewFolder;
using HarpenTech.ViewModels;

namespace HarpenTech.Views.DatabaseScreen;

public partial class DispatchViewData : ContentPage
{
    private readonly DatabaseContext _context;

    public DispatchViewData(DatabaseContext context)
    {
        _context = context;
        BindingContext = new DispatchEditViewModel(null, context);
        InitializeComponent();
    }

    // This method is the event handler for the delete button click.
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
            ((DispatchEditViewModel)BindingContext).DeleteProductCommand.Execute(e);
        }
    }
}