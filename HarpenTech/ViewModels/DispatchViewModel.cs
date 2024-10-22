using CommunityToolkit.Mvvm.Input;
using HarpenTech.NewFolder;
using HarpenTech.Services;
using HarpenTech.ViewModels.Base;
using HarpenTech.Views.Dispatch;

namespace HarpenTech.ViewModels
{
    // Partial class for the DispatchViewModel, extending ViewModelBase
    public partial class DispatchViewModel : ViewModelBase
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Constructor for DispatchViewModel, taking INavigationService and DatabaseContext as parameters.
        /// </summary>
        /// <param name="navigationService">Navigation service for navigating within the application.</param>
        /// <param name="context">Database context for interacting with the underlying data store.</param>
        public DispatchViewModel(INavigationService navigationService, DatabaseContext context) : base(navigationService)
        {
            _context = context;
        }

        /// <summary>
        /// RelayCommand method for handling the dispatch edit button click
        /// </summary>
        [RelayCommand]
        public async Task DispatchEditClickAsync()
        {
            DispatchEditViewModel dispatchEditViewModel = new DispatchEditViewModel(NavigationService,_context);
            await Shell.Current.Navigation.PushAsync(new DispatchEditView(dispatchEditViewModel,_context));
        }

        /// <summary>
        /// Represents a method that handles the asynchronous click event for navigating back.
        /// </summary>
        [RelayCommand]
        public async Task BackClickAsync()
        {
            // Use the NavigationService to navigate asynchronously to the HomePage.
            await NavigationService.NavigateToAsync("//HomePage");
        }

    }
}