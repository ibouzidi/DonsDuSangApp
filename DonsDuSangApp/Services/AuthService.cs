using DonsDuSangApp.Context.Models;
using System.Collections.Generic;
using System.Linq;

namespace DonsDuSangApp.Services
{
    public class AuthService
    {
        private readonly List<Donneur> _donneurs;

        public AuthService(List<Donneur> donneurs)
        {
            _donneurs = donneurs;
        }

        public bool VerifierConnexion(string email, string motdepasse)
        {
            var donneur = _donneurs.FirstOrDefault(d => d.Email == email);
            return donneur != null && donneur.Motdepasse == motdepasse;
        }
    }
}
