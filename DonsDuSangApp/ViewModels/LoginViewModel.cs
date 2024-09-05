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
            // Logique d'authentification
            var existingDonneur = await DbContext.Donneurs
                .FirstOrDefaultAsync(d => d.Email == Email);

            if (existingDonneur == null || !BCrypt.Net.BCrypt.Verify(Password, existingDonneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Email ou mot de passe incorrect.", "OK");
                return;
            }

            // Efface les données utilisateurs
            DbContext.ChangeTracker.Clear();

            // Marque l'utilisateur
            Preferences.Set("IsUserAuthenticated", true);
            Preferences.Set("LoggedInDonorId", existingDonneur.IdDonneur); // Enregistre l'ID de l'utilisateur

            await DialogService.DisplayAlertAsync("Succès", "Connexion réussie.", "OK");

            // Redirection au questionnaire après authentification
            await NavigationService.GoToAsync(nameof(QuestionnairePage));
        }

    }
}
