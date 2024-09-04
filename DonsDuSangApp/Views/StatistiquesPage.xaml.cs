namespace DonsDuSangApp.Views
{
    public partial class StatistiquesPage : ContentPage
    {
        public StatistiquesPage(StatistiquesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Statistique";
            SetBinding(Page.TitleProperty, new Binding(nameof(StatistiquesViewModel.Title)));

        }
    }
}
