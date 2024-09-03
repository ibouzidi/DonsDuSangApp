using System.Reflection;

namespace DonsDuSangApp.Views
{
    public partial class EventsPage : ContentPage
    {
        public EventsPage(EventsViewModel viewModel)
        {
            InitializeComponent();
            var version = typeof(MauiApp).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            VersionLabel.Text = $".NET MAUI ver. {version?[..version.IndexOf('+')]}";
            BindingContext = viewModel;
            viewModel.Title = "Calendar";
            SetBinding(Page.TitleProperty, new Binding(nameof(EventsViewModel.Title)));
        }
    }
}
