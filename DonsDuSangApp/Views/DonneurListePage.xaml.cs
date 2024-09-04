namespace DonsDuSangApp.Views
{
    public partial class DonneurListePage : ContentPage
    {
        public DonneurListePage(DonneurListeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Donneur liste";
            SetBinding(Page.TitleProperty, new Binding(nameof(DonneurListeViewModel.Title)));

        }
    }
}
