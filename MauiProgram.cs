using iMate.ViewModels;
using iMate.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

namespace iMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
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
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<TaskPage>();
            builder.Services.AddSingleton<JournalPage>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<BreathePage>();
            builder.Services.AddSingleton<MeditatePage>();
            builder.Services.AddSingleton<SliderPage>();    

            builder.Services.AddTransient<ChatViewModel>();
            builder.Services.AddTransient<DeckViewModel>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
