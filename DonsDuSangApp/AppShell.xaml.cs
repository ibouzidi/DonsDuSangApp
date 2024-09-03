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
            Preferences.Set("IsUserAuthenticated", false); // Handle logout
            await _navigationService.GoToAsync(nameof(LoginPage)); // Clear navigation stack
        }
    }

}
