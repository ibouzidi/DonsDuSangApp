using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace DonsDuSangApp
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;

        public static DonsDuSangContext DbContext { get; set; } = new DonsDuSangContext();


        public AppShell(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(DonneurRegistrationPage), typeof(DonneurRegistrationPage));
            Routing.RegisterRoute(nameof(QuestionnairePage), typeof(QuestionnairePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(AccueilPage), typeof(AccueilPage));
            Routing.RegisterRoute(nameof(MedecinLoginPage), typeof(MedecinLoginPage));
            Routing.RegisterRoute(nameof(DonneurListePage), typeof(DonneurListePage));
            Routing.RegisterRoute(nameof(DonneurDetailPage), typeof(DonneurDetailPage));
            Routing.RegisterRoute(nameof(StatistiquesPage), typeof(StatistiquesPage));
            Routing.RegisterRoute(nameof(ConsentementPage), typeof(ConsentementPage));
            Routing.RegisterRoute(nameof(DonneurSearchPage), typeof(DonneurSearchPage));
        }

        private void SetInitialRoute()
        {
            if (IsUserAuthenticated())
            {
                _navigationService.GoToAsync("//QuestionnairePage");
            }
            else
            {
                _navigationService.GoToAsync("//AccueilPage");
            }
        }

        private bool IsUserAuthenticated()
        {
            return Preferences.Get("IsUserAuthenticated", false);
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            // Efface les préférences après
            Preferences.Remove("IsUserAuthenticated");
            Preferences.Remove("LoggedInDonorId");

            // Efface les données du DbContext mises en cache.
            ClearDbContextCache();

            // Navigation à la page login
            await _navigationService.GoToAsync(nameof(LoginPage));
        }


        private void ClearDbContextCache()
        {
            // Effacement du context ou cache
            DbContext.ChangeTracker.Clear();
        }
    }

}
