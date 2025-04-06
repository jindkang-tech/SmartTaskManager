using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Services;
using SmartTaskManager.ViewModels;
using SmartTaskManager.Views;

namespace SmartTaskManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register services
        builder.Services.AddSingleton<IDataService>(s => 
            new SQLiteDataService(Path.Combine(FileSystem.AppDataDirectory, "tasks.db")));

        // Register views
        builder.Services.AddTransient<TaskListView>();
        builder.Services.AddTransient<TaskListViewModel>();

        return builder.Build();
    }
}