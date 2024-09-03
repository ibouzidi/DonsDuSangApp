namespace DonsDuSangApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            UserAppTheme = PlatformAppTheme;
        }
    }
}
