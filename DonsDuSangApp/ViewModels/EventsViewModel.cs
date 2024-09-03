namespace DonsDuSangApp.ViewModels
{
    public partial class EventsViewModel(IDialogService dialogService, INavigationService navigationService) : BaseViewModel(dialogService, navigationService)
    {
        [RelayCommand]
        private Task AddEventAsync() => NavigationService.GoToAsync("newevent");
    }
}
