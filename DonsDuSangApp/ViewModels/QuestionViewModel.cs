using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

public partial class QuestionViewModel : BaseViewModel
{
    public short IdQuestion { get; set; }

    [ObservableProperty]
    private string enonce;

    [ObservableProperty]
    private ObservableCollection<string> reponseOptions;

    [ObservableProperty]
    private string selectedReponse;

    [ObservableProperty]
    private string complementTextuel;

    [ObservableProperty]
    private bool isCritique;

    public QuestionViewModel(IDialogService dialogService, INavigationService navigationService)
        : base(dialogService, navigationService)
    {
    }

    partial void OnSelectedReponseChanged(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var loggedInDonorId = (short)Preferences.Get("LoggedInDonorId", 0);
            SaveResponseAsync(loggedInDonorId).ConfigureAwait(false);
        }
    }

    public async Task SaveResponseAsync(short loggedInDonorId)
    {
        try
        {
            if (loggedInDonorId == 0)
            {
                await DialogService.DisplayAlertAsync("Erreur", "Aucun donneur connecté.", "OK");
                return;
            }

            // Vérifie l'existence de la réponse
            var existingResponse = await DbContext.Reponses
                .FirstOrDefaultAsync(r => r.IdQuestion == IdQuestion && r.IdDonneur == loggedInDonorId);

            bool isDisqualified = IsCritique && SelectedReponse == "Non";

            if (existingResponse == null)
            {
                var response = new Reponse
                {
                    IdQuestion = IdQuestion,
                    IdDonneur = loggedInDonorId,
                    Reponse1 = SelectedReponse,
                    ComplementTextuel = ComplementTextuel,
                    EstDisqualifié = isDisqualified
                };

                await DbContext.Reponses.AddAsync(response);
            }
            else
            {
                // MAJ réponse existant
                existingResponse.Reponse1 = SelectedReponse;
                existingResponse.ComplementTextuel = ComplementTextuel;
                existingResponse.EstDisqualifié = isDisqualified;
            }

            await DbContext.SaveChangesAsync();

            if (isDisqualified)
            {
                await DialogService.DisplayAlertAsync("Information", "Vous ne pouvez pas donner de sang en raison de cette réponse.", "OK");
                await NavigationService.GoToAsync(nameof(AccueilPage));
            }
        }
        catch (Exception ex)
        {
            await DialogService.DisplayAlertAsync("Erreur", $"Erreur lors de l'enregistrement de la réponse : {ex.Message}", "OK");
        }
    }
 }
