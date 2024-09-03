using System;
using Xunit;
using DonsDuSangApp.Context.Models;

namespace DonsDuSangApp.Tests
{
    public class QuestionnaireTests
    {
        [Fact]
        public void EstTermine_QuandAccordsSontRenseignes_RetourneVrai()
        {
            // Arrange
            var questionnaire = new Questionnaire
            {
                AccordEnseignement = true,
                AccordNonTherapeutique = false
            };

            // Act
            var estTermine = questionnaire.EstTermine();

            // Assert
            Assert.True(estTermine);
        }

        [Fact]
        public void EstTermine_QuandUnAccordManque_RetourneFaux()
        {
            // Arrange
            var questionnaire = new Questionnaire
            {
                AccordEnseignement = true,
                AccordNonTherapeutique = null // Manque l'accord non thérapeutique
            };

            // Act
            var estTermine = questionnaire.EstTermine();

            // Assert
            Assert.False(estTermine);
        }

        [Fact]
        public void EstTermine_QuandLesDeuxAccordsManquent_RetourneFaux()
        {
            // Arrange
            var questionnaire = new Questionnaire
            {
                AccordEnseignement = null,
                AccordNonTherapeutique = null
            };

            // Act
            var estTermine = questionnaire.EstTermine();

            // Assert
            Assert.False(estTermine);
        }
    }
}
