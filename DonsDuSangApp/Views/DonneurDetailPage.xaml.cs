namespace DonsDuSangApp.Views
{
    public partial class DonneurDetailPage : ContentPage
    {
        public DonneurDetailPage(DonneurDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Donneur informations";
            SetBinding(Page.TitleProperty, new Binding(nameof(DonneurDetailViewModel.Title)));

        }
    }
}
