using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmartTaskManager.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    protected bool SetBusyState(bool value, string errorMessage = "")
    {
        if (value == true)
            ErrorMessage = string.Empty;

        IsBusy = value;
        IsRefreshing = value;

        if (!string.IsNullOrEmpty(errorMessage))
            ErrorMessage = errorMessage;

        return !value;
    }

    protected async Task ExecuteAsync(Func<Task> operation, string errorMessage = "")
    {
        try
        {
            SetBusyState(true);
            await operation?.Invoke();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            ErrorMessage = string.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
        }
        finally
        {
            SetBusyState(false);
        }
    }

    protected async Task<T> ExecuteAsync<T>(Func<Task<T>> operation, string errorMessage = "")
    {
        try
        {
            SetBusyState(true);
            return await operation?.Invoke();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            ErrorMessage = string.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
            return default;
        }
        finally
        {
            SetBusyState(false);
        }
    }

    [RelayCommand]
    private void ClearError() => ErrorMessage = string.Empty;
}