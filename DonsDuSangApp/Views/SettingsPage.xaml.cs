namespace DonsDuSangApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Title = "Settings";
            BindingContext = viewModel;
        }
    }
}
