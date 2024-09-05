namespace DonsDuSangApp.ViewModels
{
    public partial class AccueilViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int nombre;

        public AccueilViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {

        }

        [RelayCommand]
        private async Task AccesQuestionnaireAsync()
        {
            // Check if the user is authenticated
            if (Preferences.Get("IsUserAuthenticated", false))
            {
                await NavigationService.GoToAsync(nameof(QuestionnairePage));
            }
            else
            {
                await NavigationService.GoToAsync(nameof(DonneurRegistrationPage));
            }
        }

        [RelayCommand]
        private void Quitter() => App.Current!.Quit();


        [RelayCommand]
        private async Task AccesMedecinAsync()
        {
            // Accéder à la page de connexion du Medecin
            await NavigationService.GoToAsync(nameof(MedecinLoginPage));
        }


    }
}
