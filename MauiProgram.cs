using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using CommunityToolkit.Maui;
using TermTracker.Data;

namespace TermTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            builder.Logging.AddDebug();

            builder.Services.AddSingleton<AppDbContext>(provider =>
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "termtracker.db3");
                return new AppDbContext(dbPath);
            });

            return builder.Build();
        }
    }
}