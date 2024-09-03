namespace DonsDuSangApp.Views
{
    public partial class QuestionnairePage : ContentPage
    {
        public QuestionnairePage(QuestionnaireViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Title = "Questionnaire";
            SetBinding(Page.TitleProperty, new Binding(nameof(QuestionnaireViewModel.Title)));
        }
    }
}
