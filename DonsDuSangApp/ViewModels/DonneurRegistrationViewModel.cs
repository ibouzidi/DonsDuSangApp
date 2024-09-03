using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurRegistrationViewModel(IDialogService dialogService, INavigationService navigationService)
            : BaseViewModel(dialogService, navigationService)
    {

        [ObservableProperty]
        private Donneur _donneur = new();

        [ObservableProperty]
        private string _dateNaissanceStr;

        [RelayCommand]
        private async Task InscriptionAsync()
        {
            // Validation logic
            if (string.IsNullOrWhiteSpace(Donneur.Nom) ||
                string.IsNullOrWhiteSpace(Donneur.Prenom) ||
                string.IsNullOrWhiteSpace(DateNaissanceStr) ||
                string.IsNullOrWhiteSpace(Donneur.Email) ||
                string.IsNullOrWhiteSpace(Donneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Tous les champs doivent être remplis.", "OK");
                return;
            }

            // Convert date of birth from string to DateOnly
            if (!DateOnly.TryParse(DateNaissanceStr, out var dateNaissance))
            {
                await DialogService.DisplayAlertAsync("Erreur", "La date de naissance est invalide.", "OK");
                return;
            }

            Donneur.DateNaissance = dateNaissance;

            // Check if the email is already used
            if (await DbContext.Donneurs.AnyAsync(d => d.Email == Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Un compte avec cet email existe déjà.", "OK");
                return;
            }

            // Hash the password before saving
            Donneur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(Donneur.Motdepasse);

            // Save the new donor
            await DbContext.Donneurs.AddAsync(Donneur);
            await DbContext.SaveChangesAsync();

            // Mark the user as authenticated
            Preferences.Set("IsUserAuthenticated", true);

            await DialogService.DisplayAlertAsync("Succès", "Inscription réussie.", "OK");

            // Navigate to the Questionnaire page
            await NavigationService.GoToAsync(nameof(QuestionnairePage));
        }

        [RelayCommand]
        private async Task ConnexionAsync()
        {
            // Navigate to the LoginPage
            await NavigationService.GoToAsync(nameof(LoginPage));
        }



    }
}
