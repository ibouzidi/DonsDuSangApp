namespace DonsDuSangApp.Views
{
    public partial class ConsentementPage : ContentPage
    {
        public ConsentementPage(ConsentementViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Consentement du donneur";
            SetBinding(Page.TitleProperty, new Binding(nameof(ConsentementViewModel.Title)));

        }
    }
}
