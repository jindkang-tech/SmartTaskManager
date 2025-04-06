namespace SmartTaskManager;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for navigation
        Routing.RegisterRoute(nameof(Views.TaskDetailsView), typeof(Views.TaskDetailsView));
    }
}