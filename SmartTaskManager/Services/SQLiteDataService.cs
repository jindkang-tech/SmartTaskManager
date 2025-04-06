using SQLite;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services;

public class SQLiteDataService : IDataService
{
    private SQLiteAsyncConnection _database;
    private bool _isInitialized = false;

    public SQLiteDataService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
    }

    private async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            if (!File.Exists(_database.DatabasePath))
            {
                await _database.CreateTablesAsync<Models.Task, Category>();
            }
            _isInitialized = true;
        }
    }

    public async Task<List<Models.Task>> GetTasksAsync()
    {
        await InitializeAsync();
        return await _database.Table<Models.Task>().ToListAsync();
    }

    public async Task<Models.Task> GetTaskAsync(int id)
    {
        await InitializeAsync();
        return await _database.Table<Models.Task>().Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> AddTaskAsync(Models.Task task)
    {
        await InitializeAsync();
        return await _database.InsertAsync(task);
    }

    public async Task<int> UpdateTaskAsync(Models.Task task)
    {
        await InitializeAsync();
        return await _database.UpdateAsync(task);
    }

    public async Task<int> DeleteTaskAsync(Models.Task task)
    {
        await InitializeAsync();
        return await _database.DeleteAsync(task);
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        await InitializeAsync();
        return await _database.Table<Category>().ToListAsync();
    }

    public async Task<Category> GetCategoryAsync(int id)
    {
        await InitializeAsync();
        return await _database.Table<Category>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> AddCategoryAsync(Category category)
    {
        await InitializeAsync();
        return await _database.InsertAsync(category);
    }

    public async Task<int> UpdateCategoryAsync(Category category)
    {
        await InitializeAsync();
        return await _database.UpdateAsync(category);
    }

    public async Task<int> DeleteCategoryAsync(Category category)
    {
        await InitializeAsync();
        return await _database.DeleteAsync(category);
    }

    public async Task<List<Models.Task>> GetTasksByCategoryAsync(int categoryId)
    {
        await InitializeAsync();
        return await _database.Table<Models.Task>().Where(t => t.CategoryId == categoryId).ToListAsync();
    }

    public async Task<List<Models.Task>> GetTasksByStatusAsync(TaskStatus status)
    {
        await InitializeAsync();
        return await _database.Table<Models.Task>().Where(t => t.Status == status).ToListAsync();
    }

    public async Task<List<Models.Task>> GetTasksByPriorityAsync(TaskPriority priority)
    {
        await InitializeAsync();
        return await _database.Table<Models.Task>().Where(t => t.Priority == priority).ToListAsync();
    }

    public async Task<List<Models.Task>> SearchTasksAsync(string searchTerm)
    {
        await InitializeAsync();
        return await _database.Table<Models.Task>()
            .Where(t => t.Title.ToLower().Contains(searchTerm.ToLower()) || 
                       t.Description.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
}