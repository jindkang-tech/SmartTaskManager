using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;
using System.Collections.ObjectModel;

namespace SmartTaskManager.ViewModels;

public partial class TaskListViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Models.Task> tasks;

    [ObservableProperty]
    private ObservableCollection<Category> categories;

    [ObservableProperty]
    private Models.Task selectedTask;

    [ObservableProperty]
    private Category selectedCategory;

    [ObservableProperty]
    private string searchQuery = string.Empty;

    [ObservableProperty]
    private TaskStatus? statusFilter;

    [ObservableProperty]
    private TaskPriority? priorityFilter;

    [ObservableProperty]
    private bool showFilters;

    public ObservableCollection<TaskStatus> TaskStatuses { get; } = new();
    public ObservableCollection<TaskPriority> TaskPriorities { get; } = new();

    public TaskListViewModel(IDataService dataService)
    {
        Title = "Tasks";
        _dataService = dataService;
        Tasks = new ObservableCollection<Models.Task>();
        Categories = new ObservableCollection<Category>();

        // Initialize enum collections
        foreach (TaskStatus status in Enum.GetValues(typeof(TaskStatus)))
        {
            TaskStatuses.Add(status);
        }

        foreach (TaskPriority priority in Enum.GetValues(typeof(TaskPriority)))
        {
            TaskPriorities.Add(priority);
        }
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        await ExecuteAsync(async () =>
        {
            var taskList = await _dataService.GetTasksAsync();
            var categoryList = await _dataService.GetCategoriesAsync();

            // Update categories first as tasks depend on them
            Categories.Clear();
            foreach (var category in categoryList)
            {
                Categories.Add(category);
            }

            // Update tasks and link to categories
            Tasks.Clear();
            foreach (var task in taskList)
            {
                task.Category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                Tasks.Add(task);
            }
        });
    }

    [RelayCommand]
    private async Task DeleteTaskAsync(Models.Task task)
    {
        if (task == null)
            return;

        bool answer = await Application.Current.MainPage.DisplayAlert(
            "Delete Task",
            $"Are you sure you want to delete '{task.Title}'?",
            "Yes", "No");

        if (answer)
        {
            await ExecuteAsync(async () =>
            {
                await _dataService.DeleteTaskAsync(task);
                Tasks.Remove(task);
            });
        }
    }

    [RelayCommand]
    private async Task SearchTasksAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            await LoadDataAsync();
            return;
        }

        await ExecuteAsync(async () =>
        {
            var results = await _dataService.SearchTasksAsync(SearchQuery);
            Tasks.Clear();
            foreach (var task in results)
            {
                task.Category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                Tasks.Add(task);
            }
        });
    }

    [RelayCommand]
    private async Task FilterTasksAsync()
    {
        await ExecuteAsync(async () =>
        {
            var allTasks = await _dataService.GetTasksAsync();
            var filteredTasks = allTasks.AsEnumerable();

            if (SelectedCategory != null)
            {
                filteredTasks = filteredTasks.Where(t => t.CategoryId == SelectedCategory.Id);
            }

            if (StatusFilter.HasValue)
            {
                filteredTasks = filteredTasks.Where(t => t.Status == StatusFilter.Value);
            }

            if (PriorityFilter.HasValue)
            {
                filteredTasks = filteredTasks.Where(t => t.Priority == PriorityFilter.Value);
            }

            Tasks.Clear();
            foreach (var task in filteredTasks)
            {
                task.Category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                Tasks.Add(task);
            }
        });
    }

    [RelayCommand]
    private void ClearFilters()
    {
        SelectedCategory = null;
        StatusFilter = null;
        PriorityFilter = null;
        SearchQuery = string.Empty;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private void ShowFiltersToggle()
    {
        ShowFilters = !ShowFilters;
    }

    [RelayCommand]
    private async Task MarkTaskCompletedAsync(Models.Task task)
    {
        if (task == null)
            return;

        await ExecuteAsync(async () =>
        {
            task.Status = TaskStatus.Completed;
            task.CompletedAt = DateTime.UtcNow;
            await _dataService.UpdateTaskAsync(task);
        });
    }

    [RelayCommand]
    private async Task AddTaskAsync()
    {
        // Navigation will be implemented when we add the navigation service
        await Application.Current.MainPage.DisplayAlert("Coming Soon", "Task creation will be implemented in the next update.", "OK");
    }

    partial void OnSelectedTaskChanged(Models.Task value)
    {
        if (value != null)
        {
            // Navigation will be implemented when we add the navigation service
            SelectedTask = null; // Reset selection
        }
    }

    partial void OnSelectedCategoryChanged(Category value)
    {
        FilterTasksCommand.Execute(null);
    }

    partial void OnStatusFilterChanged(TaskStatus? value)
    {
        FilterTasksCommand.Execute(null);
    }

    partial void OnPriorityFilterChanged(TaskPriority? value)
    {
        FilterTasksCommand.Execute(null);
    }
}