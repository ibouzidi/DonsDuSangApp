namespace DonsDuSangApp.Views
{
    public partial class AccueilPage : ContentPage
    {
        public AccueilPage(AccueilViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Accueil";
            SetBinding(Page.TitleProperty, new Binding(nameof(AccueilViewModel.Title)));

        }
    }
}
