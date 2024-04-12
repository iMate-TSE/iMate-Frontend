using iMate.Services;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;

namespace iMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    // adding all the fonts
                    fonts.AddFont("Amiko-Bold.ttf", "AmikoBold");
                    fonts.AddFont("Amiko-Regular.ttf", "AmikoRegular");
                    fonts.AddFont("Amiko-SemiBold.ttf", "AmikoSemiBold");
                    fonts.AddFont("AveriaGruesaLibre-Regular.ttf", "AveriaRegular");
                    fonts.AddFont("Rubik-Regular.tff", "RubikRegular");
                    fonts.AddFont("Rubik-SemiBold.tff", "RubikSemiBold");
                });
            // Add all the pages here as a singleton (This is the inversion of control principle 
            // namely dependency injection 
            // https://learn.microsoft.com/en-us/dotnet/architecture/maui/dependency-injection
            builder.Services.AddSingleton<IHttpService, HttpService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<TaskPage>();
            builder.Services.AddSingleton<JournalPage>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<BreathePage>();
            builder.Services.AddSingleton<SliderPage>();
            builder.Services.AddSingleton<FormPage>();
            builder.Services.AddSingleton<ResourcesPage>();
            builder.Services.AddSingleton<PersonalInfoPage>();


            builder.Services.AddTransient<ChatViewModel>();
            builder.Services.AddTransient<DeckViewModel>();
            builder.Services.AddTransient<HeaderViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<PersonalInfoViewModel>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
