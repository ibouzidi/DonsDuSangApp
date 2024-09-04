using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurDetailViewModel : BaseViewModel, IQueryAttributable
{
    public ObservableCollection<Reponse> Responses { get; private set; } = new ObservableCollection<Reponse>();

    public DonneurDetailViewModel(IDialogService dialogService, INavigationService navigationService)
        : base(dialogService, navigationService)
    {
    }

    [ObservableProperty]
    private Donneur _donneur;


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("donneurId"))
        {
            int donneurId = Convert.ToInt32(query["donneurId"]);
            LoadDonneurDetailsAsync(donneurId);
        }
    }

    private async Task LoadDonneurDetailsAsync(int donneurId)
    {
        var donneur = await DbContext.Donneurs
            .Include(d => d.Reponses)
            .ThenInclude(r => r.IdQuestionNavigation)
            .FirstOrDefaultAsync(d => d.IdDonneur == donneurId);

        Donneur = donneur;

        if (donneur != null)
        {
            Responses.Clear();
            foreach (var response in donneur.Reponses)
            {
                Responses.Add(response);
            }
        }
    }
}

}
