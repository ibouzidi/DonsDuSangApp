using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurListeViewModel : BaseViewModel
    {
        public ObservableCollection<Donneur> Donneurs { get; private set; } = new ObservableCollection<Donneur>();

        public DonneurListeViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
            LoadDonneursAsync();
        }

        private async Task LoadDonneursAsync()
        {
            var donneurs = await DbContext.Donneurs
                    .Include(d => d.Reponses)
                    .Where(d => d.Reponses.Any()) // Ne rechercher que les donateurs ayant au moins une réponse
                    .ToListAsync();

            foreach (var donneur in donneurs)
            {
                Donneurs.Add(donneur);
            }
        }

        [RelayCommand]
        private async Task ViewDetailAsync(int donneurId)
        {
            await NavigationService.GoToAsync($"{nameof(DonneurDetailPage)}?donneurId={donneurId}");
        }

        [RelayCommand]
        private async Task ViewStatisticsAsync()
        {
            await NavigationService.GoToAsync(nameof(StatistiquesPage));
        }

        [RelayCommand]
        private async Task ViewSearchDonneurAsync()
        {
            await NavigationService.GoToAsync(nameof(DonneurSearchPage));
        }
    }


}
