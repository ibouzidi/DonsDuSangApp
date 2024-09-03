using System;
using System.Collections.Generic;

namespace DonsDuSangApp.Context.Models;

public partial class Donneur
{
    public short IdDonneur { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateOnly? DateNaissance { get; set; }

    public string? Email { get; set; }

    public string? Motdepasse { get; set; }

    public virtual ICollection<Questionnaire> Questionnaires { get; set; } = new List<Questionnaire>();

    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();

    public class AuthService
    {
        private readonly List<Donneur> _donneurs;

        public AuthService(List<Donneur> donneurs)
        {
            _donneurs = donneurs;
        }

        public bool SeConnecter(string email, string motDePasse)
        {
            var donneur = _donneurs.FirstOrDefault(d => d.Email == email);
            if (donneur == null) return false;

            // Pour des raisons de sécurité, il est recommandé de hasher le mot de passe et de le comparer avec celui stocké en base
            return donneur.Motdepasse == motDePasse;
        }
    }

    public class DonneurService
    {
        public bool EstMajeur(Donneur donneur)
        {
            if (donneur.DateNaissance.HasValue)
            {
                var today = DateTime.Today;
                var birthDate = donneur.DateNaissance.Value.ToDateTime(new TimeOnly(0, 0));
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;
                return age >= 18;
            }
            return false;
        }
    }

}
