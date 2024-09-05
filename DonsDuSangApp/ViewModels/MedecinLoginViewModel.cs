using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace DonsDuSangApp.ViewModels
{
    public partial class MedecinLoginViewModel(IDialogService dialogService, INavigationService navigationService)
            : BaseViewModel(dialogService, navigationService)
    {
        private const string HardcodedPassword = "123"; // Mot de passe partagé codé en dur

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task LoginAsync()
        {
            // Vérifier si le mot de passe saisi correspond au mot de passe codé en dur
            if (Password == HardcodedPassword)
            {
                // Navigation à la liste des donneurs
                await NavigationService.GoToAsync(nameof(DonneurListePage));
            }
            else
            {
                // Afficher un message d'erreur si le mot de passe est incorrect
                await DialogService.DisplayAlertAsync("Erreur", "Mot de passe incorrect", "OK");
            }
        }
    }

}
