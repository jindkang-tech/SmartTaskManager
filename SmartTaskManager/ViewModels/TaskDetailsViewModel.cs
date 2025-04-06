using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;
using System.Collections.ObjectModel;

namespace SmartTaskManager.ViewModels;

[QueryProperty("TaskId", "id")]
public partial class TaskDetailsViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private int taskId;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private DateTime dueDate = DateTime.Now.AddDays(1);

    [ObservableProperty]
    private TaskPriority priority;

    [ObservableProperty]
    private TaskStatus status;

    [ObservableProperty]
    private Category selectedCategory;

    [ObservableProperty]
    private ObservableCollection<Category> categories;

    public ObservableCollection<TaskPriority> Priorities { get; } = new();
    public ObservableCollection<TaskStatus> Statuses { get; } = new();

    public TaskDetailsViewModel(IDataService dataService)
    {
        Title = "Task Details";
        _dataService = dataService;
        Categories = new ObservableCollection<Category>();

        // Initialize enum collections
        foreach (TaskPriority priority in Enum.GetValues(typeof(TaskPriority)))
        {
            Priorities.Add(priority);
        }

        foreach (TaskStatus status in Enum.GetValues(typeof(TaskStatus)))
        {
            Statuses.Add(status);
        }
    }

    public async Task InitializeAsync()
    {
        await LoadCategoriesAsync();
        
        if (TaskId != 0)
        {
            await LoadTaskAsync();
        }
    }

    private async Task LoadCategoriesAsync()
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

    private async Task LoadTaskAsync()
    {
        await ExecuteAsync(async () =>
        {
            var task = await _dataService.GetTaskAsync(TaskId);
            if (task != null)
            {
                Title = task.Title;
                Description = task.Description;
                DueDate = task.DueDate;
                Priority = task.Priority;
                Status = task.Status;
                SelectedCategory = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
            }
        });
    }

    [RelayCommand]
    private async Task SaveTaskAsync()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Title is required", "OK");
            return;
        }

        await ExecuteAsync(async () =>
        {
            var task = new Models.Task
            {
                Id = TaskId,
                Title = Title,
                Description = Description ?? string.Empty,
                DueDate = DueDate,
                Priority = Priority,
                Status = Status,
                CategoryId = SelectedCategory?.Id ?? 0,
                Category = SelectedCategory
            };

            if (TaskId == 0)
            {
                task.CreatedAt = DateTime.UtcNow;
                await _dataService.AddTaskAsync(task);
            }
            else
            {
                await _dataService.UpdateTaskAsync(task);
            }

            await Shell.Current.GoToAsync("..");
        });
    }

    [RelayCommand]
    private async Task DeleteTaskAsync()
    {
        if (TaskId == 0)
            return;

        bool answer = await Application.Current.MainPage.DisplayAlert(
            "Delete Task",
            "Are you sure you want to delete this task?",
            "Yes", "No");

        if (answer)
        {
            await ExecuteAsync(async () =>
            {
                var task = new Models.Task { Id = TaskId };
                await _dataService.DeleteTaskAsync(task);
                await Shell.Current.GoToAsync("..");
            });
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}