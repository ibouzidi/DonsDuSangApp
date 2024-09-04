namespace DonsDuSangApp.Views
{
    public partial class MedecinLoginPage : ContentPage
    {
        public MedecinLoginPage(MedecinLoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Médecin login";
            SetBinding(Page.TitleProperty, new Binding(nameof(MedecinLoginViewModel.Title)));

        }
    }
}
