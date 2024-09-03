using DonsDuSangApp.Context.Models;
namespace DonsDuSangApp.ViewModels
{
    public partial class BaseViewModel(IDialogService dialogService, INavigationService navigationService) : ObservableObject
    {

        public static DonsDuSangContext DbContext { get; set; } = new DonsDuSangContext();
        public IDialogService DialogService => dialogService;

        public INavigationService NavigationService => navigationService;

        [ObservableProperty]
        private string _title = string.Empty;
    }
}
