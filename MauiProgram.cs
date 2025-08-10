using Pomocnik.Data;
using System.IO;

namespace Pomocnik;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Ścieżka do pliku bazy w katalogu aplikacji
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "pomocnik.db3");

        // Rejestracja jako Singleton (jedna baza na całe życie aplikacji)
        builder.Services.AddSingleton(new AppDatabase(dbPath));

        return builder.Build();
    }
}
