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

        // Use DateTime to handle DatePicker input, but convert to DateOnly when saving to the database.
        [ObservableProperty]
        private DateTime _dateNaissance = DateTime.Now;

        [RelayCommand]
        private async Task InscriptionAsync()
        {
            // Validate the input fields
            if (string.IsNullOrWhiteSpace(Donneur.Nom) ||
                string.IsNullOrWhiteSpace(Donneur.Prenom) ||
                string.IsNullOrWhiteSpace(Donneur.Email) ||
                string.IsNullOrWhiteSpace(Donneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Tous les champs doivent être remplis.", "OK");
                return;
            }

            // Convert DateNaissance from DateTime to DateOnly before saving
            Donneur.DateNaissance = DateOnly.FromDateTime(DateNaissance);

            // Check for an existing account with the same email
            if (await DbContext.Donneurs.AnyAsync(d => d.Email == Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Un compte avec cet email existe déjà.", "OK");
                return;
            }

            // Hash the password
            Donneur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(Donneur.Motdepasse);

            // Save the new donor
            await DbContext.Donneurs.AddAsync(Donneur);
            await DbContext.SaveChangesAsync();

            // Mark the user as authenticated
            Preferences.Set("IsUserAuthenticated", true);
            Preferences.Set("LoggedInDonorId", Donneur.IdDonneur);

            await DialogService.DisplayAlertAsync("Succès", "Inscription réussie.", "OK");

            // Navigate to the next page
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
