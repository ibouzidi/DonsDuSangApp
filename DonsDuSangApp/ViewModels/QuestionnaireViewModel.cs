﻿using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace DonsDuSangApp.ViewModels
{
    public partial class QuestionnaireViewModel : BaseViewModel
    {
        public ObservableCollection<QuestionViewModel> Questions { get; private set; } = new ObservableCollection<QuestionViewModel>();

        public QuestionnaireViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
            LoadQuestionsAsync();
        }

        private async void LoadQuestionsAsync()
        {
            var loggedInDonorId = (short)Preferences.Get("LoggedInDonorId", 0);
            if (loggedInDonorId == 0)
            {
                await DialogService.DisplayAlertAsync("Erreur", "Aucun donneur connecté.", "OK");
                return;
            }

            // Effacer les questions existantes pour éviter d'utiliser les données de l'utilisateur précédent
            Questions.Clear();

            // Récupérer les réponses existantes de l'utilisateur connecté
            var existingResponses = await DbContext.Reponses
                .Where(r => r.IdDonneur == loggedInDonorId)
                .ToListAsync();

            // Vérifier si l'utilisateur a une réponse disqualifiante
            var disqualifyingResponse = existingResponses.FirstOrDefault(r => r.EstDisqualifié == true);

            if (disqualifyingResponse != null)
            {
                bool situationChanged = await DialogService.DisplayAlertAsync(
                    "Changement de situation",
                    "Lors de votre dernier questionnaire, vous avez été disqualifié. Votre situation a-t-elle changé?",
                    "Oui", "Non");

                if (situationChanged)
                {
                    DbContext.Reponses.RemoveRange(existingResponses.Where(r => r.EstDisqualifié == true));
                    await DbContext.SaveChangesAsync();

                    existingResponses = await DbContext.Reponses
                        .Where(r => r.IdDonneur == loggedInDonorId)
                        .ToListAsync();
                }
                else
                {
                    await DialogService.DisplayAlertAsync("Information", "Vous ne pouvez pas donner de sang en raison de votre réponse précédente.", "OK");
                    await NavigationService.GoToAsync(nameof(AccueilPage));
                    return;
                }
            }

            // Charger les questions de la base de données
            var questions = await DbContext.Questions.Take(3).ToListAsync();

            foreach (var question in questions)
            {
                var existingResponse = existingResponses.FirstOrDefault(r => r.IdQuestion == question.IdQuestion);

                var questionVm = new QuestionViewModel(DialogService, NavigationService)
                {
                    IdQuestion = question.IdQuestion,
                    Enonce = question.Enonce,
                    ReponseOptions = new ObservableCollection<string> { "Oui", "Non", "Je ne sais pas" },
                    IsCritique = question.EstCritique ?? false,
                    SelectedReponse = existingResponse?.Reponse1,
                    ComplementTextuel = existingResponse?.ComplementTextuel
                };

                Questions.Add(questionVm);
            }
        }

        [RelayCommand]
        public async Task SubmitAsync()
        {
            try
            {
                var loggedInDonorId = (short)Preferences.Get("LoggedInDonorId", 0);
                if (Questions.Count == 0)
                {
                    await DialogService.DisplayAlertAsync("Erreur", "Le questionnaire ne peut pas être vide.", "OK");
                    return;
                }

                bool isDisqualified = false;
                bool needsInterview = false;

                foreach (var questionVm in Questions)
                {
                    if (string.IsNullOrEmpty(questionVm.SelectedReponse))
                    {
                        await DialogService.DisplayAlertAsync("Erreur", "Veuillez répondre à toutes les questions.", "OK");
                        return;
                    }

                    await questionVm.SaveResponseAsync(loggedInDonorId);

                    // Vérifie les réponses disqualifiantes
                    if (questionVm.IsCritique && questionVm.SelectedReponse == "Non")
                    {
                        isDisqualified = true;
                        break;
                    }

                    if (questionVm.SelectedReponse == "Je ne sais pas")
                    {
                        needsInterview = true;
                    }
                }

                string donationMessage = isDisqualified ? "Don impossible" : needsInterview ? "Dépend de l'entretien" : "Don faisable";

                // Enregistre les exigences de l'entretien dans la base de données
                var questionnaire = await DbContext.Questionnaires
                    .FirstOrDefaultAsync(q => q.IdDonneur == loggedInDonorId);

                if (questionnaire != null)
                {
                    questionnaire.BesoinEntretient = needsInterview;
                    await DbContext.SaveChangesAsync();
                }

                await DialogService.DisplayAlertAsync("Résultat", donationMessage, "OK");

                // Redirection à la page de consentement
                await NavigationService.GoToAsync(nameof(ConsentementPage));
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Erreur", $"Une erreur s'est produite lors de la soumission : {ex.Message}", "OK");
            }
        }
    }
}
