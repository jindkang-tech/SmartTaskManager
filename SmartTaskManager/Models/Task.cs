using SQLite;
using System.ComponentModel;

namespace SmartTaskManager.Models;

public class Task : INotifyPropertyChanged
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    private string title;
    public string Title
    {
        get => title;
        set
        {
            if (title != value)
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }

    private string description;
    public string Description
    {
        get => description;
        set
        {
            if (description != value)
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }

    private DateTime dueDate;
    public DateTime DueDate
    {
        get => dueDate;
        set
        {
            if (dueDate != value)
            {
                dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }
    }

    private TaskPriority priority;
    public TaskPriority Priority
    {
        get => priority;
        set
        {
            if (priority != value)
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
    }

    private TaskStatus status;
    public TaskStatus Status
    {
        get => status;
        set
        {
            if (status != value)
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }

    private int categoryId;
    public int CategoryId
    {
        get => categoryId;
        set
        {
            if (categoryId != value)
            {
                categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }
    }

    [Ignore]
    public Category Category { get; set; }

    private DateTime createdAt;
    public DateTime CreatedAt
    {
        get => createdAt;
        set
        {
            if (createdAt != value)
            {
                createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }
    }

    private DateTime? completedAt;
    public DateTime? CompletedAt
    {
        get => completedAt;
        set
        {
            if (completedAt != value)
            {
                completedAt = value;
                OnPropertyChanged(nameof(CompletedAt));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}