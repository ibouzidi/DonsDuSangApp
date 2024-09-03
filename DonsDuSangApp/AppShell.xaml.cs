namespace DonsDuSangApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            SetInitialRoute();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(NewEventPage), typeof(NewEventPage));
            Routing.RegisterRoute(nameof(DonneurRegistrationPage), typeof(DonneurRegistrationPage));
            Routing.RegisterRoute(nameof(QuestionnairePage), typeof(QuestionnairePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(AccueilPage), typeof(AccueilPage));
        }

        private void SetInitialRoute()
        {
            if (IsUserAuthenticated())
            {
                GoToAsync("//QuestionnairePage");
            }
            else
            {
                GoToAsync("//AccueilPage");
            }
        }

        private bool IsUserAuthenticated()
        {
            return Preferences.Get("IsUserAuthenticated", false);
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Preferences.Set("IsUserAuthenticated", false); // Handle logout
            await GoToAsync("//login");
        }
    }
}
