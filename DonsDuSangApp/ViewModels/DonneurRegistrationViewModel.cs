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
        private string _dateNaissance;  // UI binding for birthdate as a string

        // Command for registration
        [RelayCommand]
        private async Task InscriptionAsync()
        {
            // Ensure the date of birth is valid and the donor is at least 18 years old
            if (!DateTime.TryParse(_dateNaissance, out DateTime dateNaissance) || !EstMajeur(dateNaissance))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Vous devez avoir au moins 18 ans pour vous inscrire.", "OK");
                return;
            }

            // Validation for last name
            if (string.IsNullOrWhiteSpace(Donneur.Nom) || !IsNameValid(Donneur.Nom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le nom doit être valide et ne contenir que des lettres et des espaces.", "OK");
                return;
            }

            // Validation for first name
            if (string.IsNullOrWhiteSpace(Donneur.Prenom) || !IsNameValid(Donneur.Prenom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le prénom doit être valide et ne contenir que des lettres et des espaces.", "OK");
                return;
            }

            // Validation for email
            if (string.IsNullOrWhiteSpace(Donneur.Email) || !IsEmailValid(Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "L'adresse email n'est pas valide.", "OK");
                return;
            }

            // Validation for password
            if (string.IsNullOrWhiteSpace(Donneur.Motdepasse) || !IsPasswordValid(Donneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le mot de passe doit contenir au moins 6 caractères, dont une lettre, un chiffre et un caractère spécial.", "OK");
                return;
            }

            // Check if the email is already used
            if (await DbContext.Donneurs.AnyAsync(d => d.Email == Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Un compte avec cet email existe déjà.", "OK");
                return;
            }

            // Hash the password before saving
            Donneur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(Donneur.Motdepasse);

            // Save the new donor with birthdate
            Donneur.DateNaissance = DateOnly.FromDateTime(dateNaissance);
            await DbContext.Donneurs.AddAsync(Donneur);
            await DbContext.SaveChangesAsync();

            // Mark the user as authenticated
            Preferences.Set("IsUserAuthenticated", true);
            Preferences.Set("LoggedInDonorId", Donneur.IdDonneur);

            await DialogService.DisplayAlertAsync("Succès", "Inscription réussie.", "OK");

            // Navigate to the questionnaire page
            await NavigationService.GoToAsync(nameof(QuestionnairePage));
        }


        [RelayCommand]
        private async Task ConnexionAsync()
        {
            await NavigationService.GoToAsync(nameof(LoginPage));  // Navigate to LoginPage
        }

        // Password validation method
        private static bool IsPasswordValid(string password)
        {
            if (password.Length < 6) return false;
            if (!Regex.IsMatch(password, @"[A-Za-z]")) return false;  // At least one letter
            if (!Regex.IsMatch(password, @"\d")) return false;  // At least one digit
            if (!Regex.IsMatch(password, @"[\W_]")) return false;  // At least one special character
            return true;
        }

        // Email validation method
        private static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Name validation method
        private static bool IsNameValid(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-z\s]+$");  // Only letters and spaces
        }

        // Check if the donor is of legal age
        private static bool EstMajeur(DateTime dateNaissance)
        {
            var today = DateTime.Today;
            var age = today.Year - dateNaissance.Year;
            if (dateNaissance > today.AddYears(-age)) age--;  // If birthday hasn't happened yet
            return age >= 18;
        }

        // Date parsing method
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
