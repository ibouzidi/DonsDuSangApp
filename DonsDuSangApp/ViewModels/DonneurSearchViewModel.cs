using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace DonsDuSangApp.ViewModels
{
    public partial class DonneurSearchViewModel : BaseViewModel
    {
        public DonneurSearchViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {
            ConsentOptions = new List<string> { "Oui", "Non" };
            Donneurs = new ObservableCollection<Donneur>();
        }

        [ObservableProperty]
        private string selectedEnseignementOption;

        [ObservableProperty]
        private string selectedNonTherapeutiqueOption;

        public ObservableCollection<Donneur> Donneurs { get; }

        public List<string> ConsentOptions { get; }

        [RelayCommand]
        public async Task SearchDonneurAsync()
        {
            bool? enseignement = ConvertOption(SelectedEnseignementOption);
            bool? nonTherapeutique = ConvertOption(SelectedNonTherapeutiqueOption);

            var donors = await DbContext.Questionnaires
                .Where(q => enseignement == null || q.AccordEnseignement == enseignement)
                .Where(q => nonTherapeutique == null || q.AccordNonTherapeutique == nonTherapeutique)
                .Select(q => q.IdDonneurNavigation)
                .ToListAsync();

            Donneurs.Clear();
            foreach (var donor in donors)
            {
                Donneurs.Add(donor);
            }
        }

        private bool? ConvertOption(string option)
        {
            return option switch
            {
                "Oui" => true,
                "Non" => false,
                _ => null
            };
        }
    }
}
