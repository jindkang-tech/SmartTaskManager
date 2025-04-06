namespace SmartTaskManager.Views;

public partial class TaskListView : ContentPage
{
    public TaskListView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Refresh the task list when the page appears
        if (BindingContext is ViewModels.TaskListViewModel viewModel)
        {
            viewModel.LoadDataCommand.Execute(null);
        }
    }
}