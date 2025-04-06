namespace SmartTaskManager.Views;

public partial class CategoryListView : ContentPage
{
    public CategoryListView(ViewModels.CategoryListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is ViewModels.CategoryListViewModel viewModel)
        {
            viewModel.LoadDataCommand.Execute(null);
        }
    }
}