using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;
using System.Collections.ObjectModel;

namespace SmartTaskManager.ViewModels;

public partial class CategoryListViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Category> categories;

    [ObservableProperty]
    private Category selectedCategory;

    [ObservableProperty]
    private string newCategoryName;

    [ObservableProperty]
    private string newCategoryColor = "#000000";

    [ObservableProperty]
    private bool isAddingCategory;

    public CategoryListViewModel(IDataService dataService)
    {
        Title = "Categories";
        _dataService = dataService;
        Categories = new ObservableCollection<Category>();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        await ExecuteAsync(async () =>
        {
            var categoryList = await _dataService.GetCategoriesAsync();
            Categories.Clear();
            foreach (var category in categoryList)
            {
                Categories.Add(category);
            }
        });
    }

    [RelayCommand]
    private void ShowAddCategory()
    {
        IsAddingCategory = true;
        NewCategoryName = string.Empty;
        NewCategoryColor = "#000000";
    }

    [RelayCommand]
    private async Task AddCategoryAsync()
    {
        if (string.IsNullOrWhiteSpace(NewCategoryName))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Category name is required", "OK");
            return;
        }

        await ExecuteAsync(async () =>
        {
            var category = new Category
            {
                Name = NewCategoryName,
                Color = NewCategoryColor
            };

            await _dataService.AddCategoryAsync(category);
            Categories.Add(category);
            IsAddingCategory = false;
        });
    }

    [RelayCommand]
    private void CancelAddCategory()
    {
        IsAddingCategory = false;
    }

    [RelayCommand]
    private async Task DeleteCategoryAsync(Category category)
    {
        if (category == null)
            return;

        // Check if category has tasks
        var tasks = await _dataService.GetTasksAsync();
        if (tasks.Any(t => t.CategoryId == category.Id))
        {
            await Application.Current.MainPage.DisplayAlert(
                "Cannot Delete",
                "This category has tasks assigned to it. Please reassign or delete those tasks first.",
                "OK");
            return;
        }

        bool answer = await Application.Current.MainPage.DisplayAlert(
            "Delete Category",
            $"Are you sure you want to delete '{category.Name}'?",
            "Yes", "No");

        if (answer)
        {
            await ExecuteAsync(async () =>
            {
                await _dataService.DeleteCategoryAsync(category);
                Categories.Remove(category);
            });
        }
    }

    [RelayCommand]
    private async Task EditCategoryAsync(Category category)
    {
        if (category == null)
            return;

        string result = await Application.Current.MainPage.DisplayPromptAsync(
            "Edit Category",
            "Enter new name:",
            initialValue: category.Name);

        if (!string.IsNullOrWhiteSpace(result) && result != category.Name)
        {
            await ExecuteAsync(async () =>
            {
                category.Name = result;
                await _dataService.UpdateCategoryAsync(category);
                
                // Refresh the list to show updated data
                await LoadDataAsync();
            });
        }
    }
}