namespace SmartTaskManager.Models;

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum TaskStatus
{
    NotStarted,
    InProgress,
    OnHold,
    Completed,
    Cancelled
}