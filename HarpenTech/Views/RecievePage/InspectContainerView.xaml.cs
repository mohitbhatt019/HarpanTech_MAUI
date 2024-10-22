using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using HarpenTech.Services;
using HarpenTech.ViewModels;
using UraniumUI.Material.Controls;
namespace HarpenTech.Views.RecievePage;

public partial class InspectContainerView : ContentPage
{
    private readonly InspectContainerViewModel _viewModel;
    private readonly INavigationService _navigationService;
    private ViewCell _lastCell;

    public InspectContainerView(InspectContainerViewModel viewModel, INavigationService navigationService)
    {
        _viewModel = viewModel;
        BindingContext = new InspectContainerViewModel(navigationService);
        InitializeComponent();
        RemarksAutoTextFeild.ItemsSource = viewModel.DamageList;
    }


    //CancelClick for pop to root
    private async void CancelClick(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopToRootAsync();
    }

    //To save Data
    private async void SaveClickAsync(object sender, EventArgs e)
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
        string text = " \u2713 Data save Sucessfully";
        string actionButtonText = "";
        Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
        TimeSpan duration = TimeSpan.FromSeconds(4);

        var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

        await Task.Delay(2000);
        await snackbar.Show();
        #endregion
    }

    //To Execute when Focusing on Remark
    private void DamageFocus(object sender, FocusEventArgs e)
    {
        if (StatuslistViewData.IsVisible == true) StatuslistViewData.IsVisible = false;
        if (GradelistViewData.IsVisible == true) GradelistViewData.IsVisible = false;
        RemarkslistViewData.IsVisible = true;

    }

    // To execute when unfocusing from the Remark field
    private void RemarksUnFocus(object sender, FocusEventArgs e)
    {
        RemarkslistViewData.IsVisible = false;
    }

    // To check the Remark List when an item is clicked
    private void DamageListClick(object sender, EventArgs e)
    {



        // Cast the sender to ViewCell
        var viewCell = (ViewCell)sender;

        // Get the selected item from the BindingContext
        if (viewCell.BindingContext is string selectedDamage)
        {
            if (selectedDamage != null || selectedDamage != "")
                RemarksAutoTextFeild.Text = selectedDamage;
        }

        // Unfocus after setting the text
        RemarksAutoTextFeild.Unfocus();
        RemarkslistViewData.IsVisible = false;

    }

    //To Execute when click on StatusImage
    private void DamageImageClick(object sender, EventArgs e)
    {
        RemarksAutoTextFeild.Focus();
        if (RemarkslistViewData.IsVisible) RemarkslistViewData.IsVisible = false;
        else RemarkslistViewData.IsVisible = true;
        if (GradelistViewData.IsVisible) GradelistViewData.IsVisible = false;
        if (StatuslistViewData.IsVisible) StatuslistViewData.IsVisible = false;
    }

    //To Execute when click on Damage List
    private void DamageTestChanged(object sender, TextChangedEventArgs e)
    {
        var TextField = (AutoCompleteTextField)sender;
        if (StatuslistViewData.IsVisible == true) StatuslistViewData.IsVisible = false;
        if (TextField.Text.Length <= 1)
            RemarkslistViewData.IsVisible = true;
        else RemarkslistViewData.IsVisible = false;
    }

    //Status Dropdown

    private void StatusTestChanged(object sender, TextChangedEventArgs e)
    {
        var TextField = (AutoCompleteTextField)sender;
        if (RemarkslistViewData.IsVisible == true) RemarkslistViewData.IsVisible = false;
        if (TextField.Text.Length <= 1)
            StatuslistViewData.IsVisible = true;
        else StatuslistViewData.IsVisible = false;
    }

    private void StatusFocus(object sender, FocusEventArgs e)
    {


        if (StatuslistViewData.IsVisible == true) StatuslistViewData.IsVisible = false;
        if (RemarkslistViewData.IsVisible == true) RemarkslistViewData.IsVisible = false;

        StatuslistViewData.IsVisible = true;
    }

    private void StatusUnFocus(object sender, FocusEventArgs e)
    {
        StatuslistViewData.IsVisible = false;
    }

    private async void StatusListClick(object sender, EventArgs e)
    {

        // Cast the sender to ViewCell
        var viewCell = (ViewCell)sender;

        // Get the selected item from the BindingContext
        if (viewCell.BindingContext is string selectedStatus)
        {
            if (selectedStatus != null || selectedStatus != "")
                viewCell.View.BackgroundColor = Color.FromRgb(0.50, 0.50, 0.50);
            _lastCell = viewCell;
            StatusAutoTextFeild.Text = selectedStatus;
        }
        StatusAutoTextFeild.Unfocus();
        StatuslistViewData.IsVisible = false;
    }

    private void StatusImageClick(object sender, EventArgs e)
    {
        StatusAutoTextFeild.Focus();
        if (StatusAutoTextFeild.Focus()) StatusAutoTextFeild.Unfocus();
        else StatusAutoTextFeild.Unfocus();


        if (RemarkslistViewData.IsVisible) RemarkslistViewData.IsVisible = false;
        if (GradelistViewData.IsVisible) GradelistViewData.IsVisible = false;


    }

    //Grade Dropdown
    private void GradeListClick(object sender, EventArgs e)
    {
        // Cast the sender to ViewCell
        var viewCell = (ViewCell)sender;

        // Get the selected item from the BindingContext
        if (viewCell.BindingContext is string selectedGrade)
        {
            if (selectedGrade != null || selectedGrade != "")
                GradeAutoTextFeild.Text = selectedGrade;
        }
        GradeAutoTextFeild.Unfocus();
        GradelistViewData.IsVisible = false;
    }

    private void GradeTextChanged(object sender, TextChangedEventArgs e)
    {
        var TextField = (AutoCompleteTextField)sender;
        if (RemarkslistViewData.IsVisible == true) RemarkslistViewData.IsVisible = false;
        if (StatuslistViewData.IsVisible == true) StatuslistViewData.IsVisible = false;

        if (TextField.Text.Length <= 1)
            GradelistViewData.IsVisible = true;
        else GradelistViewData.IsVisible = false;
    }

    private void GradeFocus(object sender, FocusEventArgs e)
    {
        if (StatuslistViewData.IsVisible == true) StatuslistViewData.IsVisible = false;
        if (RemarkslistViewData.IsVisible == true) RemarkslistViewData.IsVisible = false;

        GradelistViewData.IsVisible = true;

    }

    private void GradeUnFocus(object sender, FocusEventArgs e)
    {
        GradelistViewData.IsVisible = false;
    }

    private void GradeImageClick(object sender, EventArgs e)
    {
        GradeAutoTextFeild.Focus();

        if (GradelistViewData.IsVisible) GradelistViewData.IsVisible = false;
        else GradelistViewData.IsVisible = true;
        if (RemarkslistViewData.IsVisible) RemarkslistViewData.IsVisible = false;
        if (StatuslistViewData.IsVisible) StatuslistViewData.IsVisible = false;
    }

    private void RemarksFocus(object sender, FocusEventArgs e)
    {
        RemarksEditor.Placeholder = null;
        RemarksLabel.IsVisible = true;
    }

    private void RemarksUnfocused(object sender, FocusEventArgs e)

    {
        if (RemarksEditor.Text == null || RemarksEditor.Text == "")
        {
            RemarksLabel.IsVisible = false;
            RemarksEditor.Placeholder = "Remarks";
        }
        else
        { RemarksLabel.IsVisible = true; }

    }


}