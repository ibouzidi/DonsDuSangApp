namespace DonsDuSangApp.ViewModels
{
    public partial class AccueilViewModel(IDialogService dialogService, INavigationService navigationService)
        : BaseViewModel(dialogService, navigationService)
    {

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
    }
}
