using Microsoft.Extensions.Logging;

namespace DonsDuSangApp
{
    public static partial class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .UseMauiCommunityToolkit()
                   .UseMauiCommunityToolkitMarkup()
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                       fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                       fonts.AddFont("MaterialDesign-Webfont.ttf", "MaterialDesignIcons");
                       fonts.AddFont("FontAwesome-Solid900.ttf", "FontAwesomeIcons");
                   });

            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<EventsViewModel>();
            builder.Services.AddSingleton<EventsPage>();
            builder.Services.AddSingleton<SearchViewModel>();
            builder.Services.AddSingleton<SearchPage>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddTransient<NewEventViewModel>();
            builder.Services.AddTransient<NewEventPage>();

            builder.Services.AddSingleton<AccueilViewModel>();
            builder.Services.AddSingleton<AccueilPage>();

            builder.Services.AddTransient<DonneurRegistrationViewModel>();
            builder.Services.AddTransient<DonneurRegistrationPage>();

            builder.Services.AddTransient<QuestionnaireViewModel>();
            builder.Services.AddTransient<QuestionnairePage>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
