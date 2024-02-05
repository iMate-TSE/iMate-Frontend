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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Amiko-Bold.ttf", "AmikoBold");
                    fonts.AddFont("Amiko-Regular.ttf", "AmikoRegular");
                    fonts.AddFont("Amiko-SemiBold.ttf", "AmikoSemiBold");
                    fonts.AddFont("AveriaGruesaLibre-Regular.tff", "AveriaRegular");
                    fonts.AddFont("Rubik-Regular.tff", "RubikRegular");
                    fonts.AddFont("Rubik-SemiBold.tff", "RubikSemiBold");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<TaskPage>();
            builder.Services.AddSingleton<JournalPage>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
