namespace DonsDuSangApp.Views
{
    public partial class DonneurRegistrationPage : ContentPage
    {
        public DonneurRegistrationPage(DonneurRegistrationViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Inscription du donneur de sang";
            SetBinding(Page.TitleProperty, new Binding(nameof(DonneurRegistrationViewModel.Title)));
        }
    }
}
