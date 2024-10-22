using HarpenTech.ViewModels;

namespace HarpenTech.Views.RepairPage;

public partial class RepairEditView : ContentPage
{
    /// <summary>
    /// Constructor for the RepairEditView
    /// </summary>
    /// <param name="repairEditViewModel">The ViewModel for data binding</param>
    public RepairEditView(RepairEditViewModel repairEditViewModel)
    {
        // Set the ViewModel for data binding
        BindingContext = repairEditViewModel;

        // Initialize the view components
        InitializeComponent();
    }
}
