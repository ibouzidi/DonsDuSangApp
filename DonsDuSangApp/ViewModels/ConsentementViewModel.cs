using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;


namespace DonsDuSangApp.ViewModels
{
    public partial class ConsentementViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool accordEnseignement;

        [ObservableProperty]
        private bool accordRecherche;

        [ObservableProperty]
        private bool accordNonTherapeutique;

        public ConsentementViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
        }

        [RelayCommand]
        public async Task SubmitConsentAsync()
        {
            var loggedInDonorId = (short)Preferences.Get("LoggedInDonorId", 0);

            // Find the existing questionnaire or create a new one
            var questionnaire = await DbContext.Questionnaires
                .FirstOrDefaultAsync(q => q.IdDonneur == loggedInDonorId);

            if (questionnaire == null)
            {
                questionnaire = new Questionnaire
                {
                    IdDonneur = loggedInDonorId,
                    DateCreation = DateTime.Now,
                    AccordEnseignement = AccordEnseignement,
                    AccordNonTherapeutique = AccordNonTherapeutique
                };

                await DbContext.Questionnaires.AddAsync(questionnaire);
            }
            else
            {
                questionnaire.AccordEnseignement = AccordEnseignement;
                questionnaire.AccordNonTherapeutique = AccordNonTherapeutique;
            }

            await DbContext.SaveChangesAsync();

            await DialogService.DisplayAlertAsync("Succès", "Consentement soumis avec succès.", "OK");
            await NavigationService.GoToAsync(nameof(AccueilPage));
        }
    }
}
