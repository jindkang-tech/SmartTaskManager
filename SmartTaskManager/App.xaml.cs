namespace SmartTaskManager;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(Handler.MauiContext.Services.GetService<Views.TaskListView>());
    }
}