using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace DonsDuSangApp.ViewModels
{
    public partial class MedecinLoginViewModel(IDialogService dialogService, INavigationService navigationService)
            : BaseViewModel(dialogService, navigationService)
    {
        private const string HardcodedPassword = "medecinpassword"; // Hardcoded shared password

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task LoginAsync()
        {
            // Check if the entered password matches the hardcoded password
            if (Password == HardcodedPassword)
            {
                // Navigate to the Donor List page
                await NavigationService.GoToAsync(nameof(DonneurListePage));
            }
            else
            {
                // Show an error message if the password is incorrect
                await DialogService.DisplayAlertAsync("Erreur", "Mot de passe incorrect", "OK");
            }
        }
    }

}
