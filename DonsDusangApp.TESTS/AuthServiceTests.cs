using DonsDuSangApp.Context.Models;
using System.Collections.Generic;
using Xunit;
using DonsDuSangApp.Services; // Assurez-vous que ceci est bien la bonne référence pour AuthService

namespace DonsDuSangApp.Tests
{
    public class AuthServiceTests
    {
        private List<Donneur> _donneurs;
        private DonsDuSangApp.Services.AuthService _authService; // Utilisez le nom complet ici

        public AuthServiceTests()
        {
            // Simuler une liste de donneurs
            _donneurs = new List<Donneur>
            {
                new Donneur { IdDonneur = 1, Email = "john.doe@example.com", Motdepasse = "password123" },
                new Donneur { IdDonneur = 2, Email = "jane.doe@example.com", Motdepasse = "securepassword" }
            };

            _authService = new DonsDuSangApp.Services.AuthService(_donneurs);
        }

        [Fact]
        public void VerifierConnexion_CredentialsValides_RetourneVrai()
        {
            // Act
            var resultat = _authService.VerifierConnexion("john.doe@example.com", "password123");

            // Assert
            Assert.True(resultat);
        }

        [Fact]
        public void VerifierConnexion_MotDePasseInvalide_RetourneFaux()
        {
            // Act
            var resultat = _authService.VerifierConnexion("john.doe@example.com", "mauvaismotdepasse");

            // Assert
            Assert.False(resultat);
        }

        [Fact]
        public void VerifierConnexion_EmailInvalide_RetourneFaux()
        {
            // Act
            var resultat = _authService.VerifierConnexion("invalide.email@example.com", "password123");

            // Assert
            Assert.False(resultat);
        }

        [Fact]
        public void VerifierConnexion_EmailVide_RetourneFaux()
        {
            // Act
            var resultat = _authService.VerifierConnexion("", "password123");

            // Assert
            Assert.False(resultat);
        }

        [Fact]
        public void VerifierConnexion_MotDePasseVide_RetourneFaux()
        {
            // Act
            var resultat = _authService.VerifierConnexion("john.doe@example.com", "");

            // Assert
            Assert.False(resultat);
        }
    }
}
