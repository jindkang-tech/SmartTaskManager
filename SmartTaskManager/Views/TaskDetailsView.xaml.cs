namespace SmartTaskManager.Views;

public partial class TaskDetailsView : ContentPage
{
    public TaskDetailsView(ViewModels.TaskDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is ViewModels.TaskDetailsViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}