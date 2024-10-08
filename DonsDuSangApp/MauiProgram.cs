﻿using Microsoft.Extensions.Logging;

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

            builder.Services.AddSingleton<AccueilViewModel>();
            builder.Services.AddSingleton<AccueilPage>();

            builder.Services.AddTransient<DonneurRegistrationViewModel>();
            builder.Services.AddTransient<DonneurRegistrationPage>();

            builder.Services.AddTransient<QuestionnaireViewModel>();
            builder.Services.AddTransient<QuestionnairePage>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddTransient<MedecinLoginViewModel>();
            builder.Services.AddTransient<MedecinLoginPage>();

            builder.Services.AddTransient<DonneurListeViewModel>();
            builder.Services.AddTransient<DonneurListePage>();

            builder.Services.AddTransient<DonneurDetailViewModel>();
            builder.Services.AddTransient<DonneurDetailPage>();

            builder.Services.AddTransient<StatistiquesViewModel>();
            builder.Services.AddTransient<StatistiquesPage>();

            builder.Services.AddTransient<ConsentementViewModel>();
            builder.Services.AddTransient<ConsentementPage>();
            
            builder.Services.AddTransient<DonneurSearchViewModel>();
            builder.Services.AddTransient<DonneurSearchPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
