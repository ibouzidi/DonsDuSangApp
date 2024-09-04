using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurSearchViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string selectedEnseignementOption;

        [ObservableProperty]
        private string selectedNonTherapeutiqueOption;

        public ObservableCollection<Donneur> Donneurs { get; private set; } = new ObservableCollection<Donneur>();

        // List of options for consent
        public List<string> ConsentOptions { get; } = new List<string> { "Oui", "Non" };

        public DonneurSearchViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
        }

        // Command to search for donors based on the selected consent options
        [RelayCommand]
        public async Task SearchDonneurAsync()
        {
            Donneurs.Clear();

            var query = DbContext.Questionnaires.AsQueryable();

            // Apply filter for AccordEnseignement
            if (SelectedEnseignementOption == "Oui")
            {
                query = query.Where(q => q.AccordEnseignement == true);
            }
            else if (SelectedEnseignementOption == "Non")
            {
                query = query.Where(q => q.AccordEnseignement == false);
            }

            // Apply filter for AccordNonTherapeutique
            if (SelectedNonTherapeutiqueOption == "Oui")
            {
                query = query.Where(q => q.AccordNonTherapeutique == true);
            }
            else if (SelectedNonTherapeutiqueOption == "Non")
            {
                query = query.Where(q => q.AccordNonTherapeutique == false);
            }

            // Retrieve matching donors and add them to the collection
            var donors = await query
                .Include(q => q.IdDonneurNavigation) // Ensure that we load the related Donneur data
                .Select(q => q.IdDonneurNavigation)
                .ToListAsync();

            foreach (var donor in donors)
            {
                Donneurs.Add(donor);
            }
        }
    }
}
