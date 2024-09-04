using DonsDuSangApp.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace DonsDuSangApp
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;

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
            // Clear the preferences upon logout
            Preferences.Remove("IsUserAuthenticated");
            Preferences.Remove("LoggedInDonorId");

            // Optionally, clear the DbContext cached data
            ClearDbContextCache();

            // Navigate to the login page after logging out
            await _navigationService.GoToAsync(nameof(LoginPage));
        }

        public static DonsDuSangContext DbContext { get; set; } = new DonsDuSangContext();

        private void ClearDbContextCache()
        {
            // Ensure that the context is reset, or any cached data specific to the previous user is cleared.
            DbContext.ChangeTracker.Clear(); // This clears tracked entities from the context.
        }
    }

}
