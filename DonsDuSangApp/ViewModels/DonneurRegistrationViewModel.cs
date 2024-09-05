using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurRegistrationViewModel(IDialogService dialogService, INavigationService navigationService)
            : BaseViewModel(dialogService, navigationService)
    {

        [ObservableProperty]
        private Donneur _donneur = new();

        [ObservableProperty]
        private string _dateNaissance;

        // Inscription
        [RelayCommand]
        private async Task InscriptionAsync()
        {
            // S'assurer que la date de naissance est valide et que le donneur est âgé d'au moins 18 ans.
            if (!DateTime.TryParse(_dateNaissance, out DateTime dateNaissance) || !EstMajeur(dateNaissance))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Vous devez avoir au moins 18 ans pour vous inscrire.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Donneur.Nom) || !IsNameValid(Donneur.Nom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le nom doit être valide et ne contenir que des lettres et des espaces.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Donneur.Prenom) || !IsNameValid(Donneur.Prenom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le prénom doit être valide et ne contenir que des lettres et des espaces.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Donneur.Email) || !IsEmailValid(Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "L'adresse email n'est pas valide.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Donneur.Motdepasse) || !IsPasswordValid(Donneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le mot de passe doit contenir au moins 6 caractères, dont une lettre, un chiffre et un caractère spécial.", "OK");
                return;
            }

            if (await DbContext.Donneurs.AnyAsync(d => d.Email == Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Un compte avec cet email existe déjà.", "OK");
                return;
            }

            // Hash le mot de passe
            Donneur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(Donneur.Motdepasse);

            // Enregistre le donneur avec sa date de naissance
            Donneur.DateNaissance = DateOnly.FromDateTime(dateNaissance);
            await DbContext.Donneurs.AddAsync(Donneur);
            await DbContext.SaveChangesAsync();

            // Marque l'utilisateur comme étant connecté
            Preferences.Set("IsUserAuthenticated", true);
            Preferences.Set("LoggedInDonorId", Donneur.IdDonneur);

            await DialogService.DisplayAlertAsync("Succès", "Inscription réussie.", "OK");

            // Redirection au questionnaire après inscription réussie
            await NavigationService.GoToAsync(nameof(QuestionnairePage));
        }


        [RelayCommand]
        private async Task ConnexionAsync()
        {
            await NavigationService.GoToAsync(nameof(LoginPage));
        }

        // Password validation method
        private static bool IsPasswordValid(string password)
        {
            if (password.Length < 6) return false;
            if (!Regex.IsMatch(password, @"[A-Za-z]")) return false;  // Au moins une lettre
            if (!Regex.IsMatch(password, @"\d")) return false;  //  Au moins un chiffre
            if (!Regex.IsMatch(password, @"[\W_]")) return false;  // Au moins un caractère spéciale
            return true;
        }

        // Validation de l'adresse email
        private static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Validation sur le nom
        private static bool IsNameValid(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-z\s]+$");  // Seulement lettre et espace
        }

        // Vérification de l'age du donneur (>=18)
        private static bool EstMajeur(DateTime dateNaissance)
        {
            var today = DateTime.Today;
            var age = today.Year - dateNaissance.Year;
            if (dateNaissance > today.AddYears(-age)) age--;  // Si l'anniversaire n'a pas encore eu lieu
            return age >= 18;
        }

        // Traitement de la date (format)
        private static bool TryParseDate(string dateString, out DateTime date)
        {
            var cultureInfo = CultureInfo.InvariantCulture;
            if (DateTime.TryParse(dateString, cultureInfo, DateTimeStyles.None, out DateTime dateTime))
            {
                date = dateTime;
                return true;
            }

            date = default;
            return false;
        }
    }
}
