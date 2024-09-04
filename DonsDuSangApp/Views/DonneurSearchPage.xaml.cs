namespace DonsDuSangApp.Views
{
    public partial class DonneurSearchPage : ContentPage
    {
        public DonneurSearchPage(DonneurSearchViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Recherche des donneurs";
            SetBinding(Page.TitleProperty, new Binding(nameof(DonneurSearchViewModel.Title)));
        }
    }
}
