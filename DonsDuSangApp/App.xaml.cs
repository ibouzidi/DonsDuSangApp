namespace DonsDuSangApp
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;

        public App(INavigationService navigationService)
        {
            InitializeComponent();

            _navigationService = navigationService;

            // Pass the navigation service to the AppShell
            MainPage = new AppShell(_navigationService);

            UserAppTheme = PlatformAppTheme;
        }
    }
}
