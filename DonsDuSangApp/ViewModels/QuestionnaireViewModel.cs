using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
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

            // Clear existing questions to avoid using data from the previous user
            Questions.Clear();

            // Récupère la réponse existante de l'user connecté
            var existingResponses = await DbContext.Reponses
                .Where(r => r.IdDonneur == loggedInDonorId)
                .ToListAsync();

            // Vérifie si user a une réponse disqualifiante
            var disqualifyingResponse = existingResponses.FirstOrDefault(r => r.EstDisqualifié == true);

            if (disqualifyingResponse != null)
            {
                // Demande à l'user de confirmé si sa situation a changer
                bool situationChanged = await DialogService.DisplayAlertAsync(
                    "Changement de situation",
                    "Lors de votre dernier questionnaire, vous avez été disqualifié. Votre situation a-t-elle changé?",
                    "Oui",
                    "Non");

                if (situationChanged)
                {
                    // Efface précédente réponse disqualifiante si la situation a change
                    DbContext.Reponses.RemoveRange(existingResponses.Where(r => r.EstDisqualifié == true));

                    await DbContext.SaveChangesAsync(); // Sauvegarde les changements avant de procéder

                    // Refait une récupération aprés l'effacement des réponses disqualifiantes
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

            // récupére les questions depuis la bdd
            var questions = await DbContext.Questions
                .Where(q => q.IdQuestion == 1 || q.IdQuestion == 2 || q.IdQuestion == 3)
                .ToListAsync();

            // Remplis la collection des questions avec les modelview Questions
            Questions.Clear(); // Efface toute question existante pour éviter les doublons.
            foreach (var question in questions)
            {
                // Trouve la réponse qui correspond si existe
                var existingResponse = existingResponses.FirstOrDefault(r => r.IdQuestion == question.IdQuestion);

                var questionVm = new QuestionViewModel(DialogService, NavigationService)
                {
                    IdQuestion = question.IdQuestion,
                    Enonce = question.Enonce,
                    ReponseOptions = new ObservableCollection<string> { "Oui", "Non", "Je ne sais pas" },
                    IsCritique = question.EstCritique ?? false,
                    SelectedReponse = existingResponse?.Reponse1,  // Charge les réponses précédentes
                    ComplementTextuel = existingResponse?.ComplementTextuel // Charge les compléments de réponse précédente
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

                foreach (var questionVm in Questions)
                {
                    await questionVm.SaveResponseAsync(loggedInDonorId);
                }

                await DialogService.DisplayAlertAsync("Succès", "Questionnaire soumis avec succès.", "OK");
                await NavigationService.GoToAsync(nameof(AccueilPage));
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Erreur", $"Une erreur s'est produite lors de la soumission : {ex.Message}", "OK");
            }
        }
    }
}
