using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurRegistrationViewModel : BaseViewModel
    {
        public DonneurRegistrationViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
        }

        [ObservableProperty]
        private Donneur _donneur = new();

        [ObservableProperty]
        private string _dateNaissance;  // Assurez-vous que ceci est un string venant de l'UI

        [RelayCommand]
        private async Task InscriptionAsync()
        {
            // Conversion de la chaîne de date de naissance en DateOnly
            if (!TryParseDate(_dateNaissance, out DateTime dateNaissance))
            {
                await DialogService.DisplayAlertAsync("Erreur", "La date de naissance n'est pas valide.", "OK");
                return;
            }

            // Vérifiez si l'utilisateur est majeur
            if (!EstMajeur(dateNaissance))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Vous devez être majeur pour vous inscrire.", "OK");
                return;
            }

            // Validation du nom
            if (string.IsNullOrWhiteSpace(Donneur.Nom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le nom doit être renseigné.", "OK");
                return;
            }
            else if (!IsNameValid(Donneur.Nom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le nom ne doit contenir que des lettres et des espaces.", "OK");
                return;
            }

            // Validation du prénom
            if (string.IsNullOrWhiteSpace(Donneur.Prenom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le prénom doit être renseigné.", "OK");
                return;
            }
            else if (!IsNameValid(Donneur.Prenom))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le prénom ne doit contenir que des lettres et des espaces.", "OK");
                return;
            }

            // Validation de l'email
            if (string.IsNullOrWhiteSpace(Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "L'email doit être renseigné.", "OK");
                return;
            }
            else if (!IsEmailValid(Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "L'adresse email n'est pas valide.", "OK");
                return;
            }

            // Validation du mot de passe
            if (string.IsNullOrWhiteSpace(Donneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le mot de passe doit être renseigné.", "OK");
                return;
            }
            else if (!IsPasswordValid(Donneur.Motdepasse))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Le mot de passe doit contenir au moins 6 caractères, dont une lettre, un chiffre et un caractère spécial.", "OK");
                return;
            }

            // Vérification si l'email est déjà utilisé
            if (await DbContext.Donneurs.AnyAsync(d => d.Email == Donneur.Email))
            {
                await DialogService.DisplayAlertAsync("Erreur", "Un compte avec cet email existe déjà.", "OK");
                return;
            }

            // Hash du mot de passe avant de le sauvegarder
            Donneur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(Donneur.Motdepasse);

            // Sauvegarde du nouveau donneur et de sa date de naissance
            Donneur.DateNaissance = dateNaissance; // Sauvegarder la date de naissance convertie
            await DbContext.Donneurs.AddAsync(Donneur);
            await DbContext.SaveChangesAsync();

            // Marquer l'utilisateur comme authentifié
            Preferences.Set("IsUserAuthenticated", true);

            await DialogService.DisplayAlertAsync("Succès", "Inscription réussie.", "OK");

            // Navigation vers la page du questionnaire
            await NavigationService.GoToAsync(nameof(QuestionnairePage));
        }

        [RelayCommand]
        private async Task ConnexionAsync()
        {
            // Navigation vers la LoginPage
            await NavigationService.GoToAsync(nameof(LoginPage));
        }

        private static bool IsPasswordValid(string password)
        {
            // Vérifier si le mot de passe contient au moins 6 caractères
            if (password.Length < 6)
                return false;

            // Vérifier s'il contient au moins une lettre
            if (!Regex.IsMatch(password, @"[A-Za-z]"))
                return false;

            // Vérifier s'il contient au moins un chiffre
            if (!Regex.IsMatch(password, @"\d"))
                return false;

            // Vérifier s'il contient au moins un caractère spécial
            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            // Si toutes les conditions sont remplies, le mot de passe est valide
            return true;
        }

        private static bool IsEmailValid(string email)
        {
            // Utiliser une regex pour valider le format de l'email
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private static bool IsNameValid(string name)
        {
            // Vérifier que le nom ou prénom contient uniquement des lettres et des espaces
            return Regex.IsMatch(name, @"^[A-Za-z\s]+$");
        }

        // Méthode pour vérifier si le donneur est majeur
        private static bool EstMajeur(DateTime dateNaissance)
        {
            var today = DateTime.Today; // Conversion de la date d'aujourd'hui en DateOnly
            var age = today.Year - dateNaissance.Year;

            // Si la date d'anniversaire de cette année n'est pas encore passée
            if (dateNaissance > today.AddYears(-age))
                age--;

            return age >= 18;
        }

        // Ajout de cette méthode pour parser la date correctement
        private static bool TryParseDate(string dateString, out DateTime date)
        {
            // Utilise le parsing avec plusieurs formats
            var cultureInfo = CultureInfo.InvariantCulture;

            if (DateTime.TryParse(dateString, out DateTime dateTime))
            {
                date = dateTime;
                return true;
            }

            date = default;
            return false;
        }
    }
}
