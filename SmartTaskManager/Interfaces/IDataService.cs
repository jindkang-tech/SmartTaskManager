using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces;

public interface IDataService
{
    Task<List<Models.Task>> GetTasksAsync();
    Task<Models.Task> GetTaskAsync(int id);
    Task<int> AddTaskAsync(Models.Task task);
    Task<int> UpdateTaskAsync(Models.Task task);
    Task<int> DeleteTaskAsync(Models.Task task);
    
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryAsync(int id);
    Task<int> AddCategoryAsync(Category category);
    Task<int> UpdateCategoryAsync(Category category);
    Task<int> DeleteCategoryAsync(Category category);
    
    Task<List<Models.Task>> GetTasksByCategoryAsync(int categoryId);
    Task<List<Models.Task>> GetTasksByStatusAsync(TaskStatus status);
    Task<List<Models.Task>> GetTasksByPriorityAsync(TaskPriority priority);
    Task<List<Models.Task>> SearchTasksAsync(string searchTerm);
}