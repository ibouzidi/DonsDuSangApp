namespace DonsDuSangApp.ViewModels
{
    public partial class AccueilViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int nombre;

        public AccueilViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
            // Initialize Nombre with a default value or fetch it from a service
            LoadNombreAsync();
        }

        private async Task LoadNombreAsync()
        {
            // Simulate fetching the number of donors from a data source
            // Replace this with your actual data-fetching logic
            Nombre = await Task.FromResult(1234); // Example value, replace with actual logic
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
            // Navigate to the Medecin Login page
            await NavigationService.GoToAsync(nameof(MedecinLoginPage));
        }


    }
}
