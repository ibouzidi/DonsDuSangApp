using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DonsDuSangApp.ViewModels
{
    public partial class LoginViewModel(IDialogService dialogService, INavigationService navigationService)
            : BaseViewModel(dialogService, navigationService)
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task ConnexionAsync()
        {
            // Simple login logic
            var existingDonneur = await DbContext.Donneurs
                .FirstOrDefaultAsync(d => d.Email == Email);

            if (existingDonneur == null || !BCrypt.Net.BCrypt.Verify(Password, existingDonneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Email ou mot de passe incorrect.", "OK");
                return;
            }

            // Mark the user as authenticated
            Preferences.Set("IsUserAuthenticated", true);

            await DialogService.DisplayAlertAsync("Succès", "Connexion réussie.", "OK");

            // Navigate to the Questionnaire page
            await NavigationService.GoToAsync(nameof(QuestionnairePage));
        }
    }
}
