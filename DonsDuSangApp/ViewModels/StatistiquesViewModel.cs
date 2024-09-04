using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace DonsDuSangApp.ViewModels
{
    public partial class StatistiquesViewModel : BaseViewModel
    {
        [ObservableProperty]
        private List<QuestionStatistique> _questionsStats;

        public StatistiquesViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
            LoadStatisticsAsync();
        }

        private async Task LoadStatisticsAsync()
        {
            // Get all questions from the database
            var questions = await DbContext.Questions.Include(q => q.Reponses).ToListAsync();

            var stats = questions.Select(question => new QuestionStatistique
            {
                IdQuestion = question.IdQuestion,
                Enonce = question.Enonce ?? "Sans Enoncé",
                PourcentageOui = question.Reponses.Count == 0
                    ? 0
                    : (question.Reponses.Count(r => r.Reponse1 == "Oui") / (float)question.Reponses.Count()) * 100
            }).ToList();

            QuestionsStats = stats;
        }
    }

    public class QuestionStatistique
    {
        public short IdQuestion { get; set; }
        public string Enonce { get; set; }
        public float PourcentageOui { get; set; }
    }
}