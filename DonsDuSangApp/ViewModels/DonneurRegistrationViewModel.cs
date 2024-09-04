using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
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
        private string _dateNaissanceStr;

        [RelayCommand]
        private async Task InscriptionAsync()
        {
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

            // Sauvegarde du nouveau donneur
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
    }
}
