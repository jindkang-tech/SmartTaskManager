# Smart Task Manager

A comprehensive task management application built with .NET MAUI that demonstrates advanced mobile development skills and best practices.

## Features

- User Authentication and Authorization
- Cloud Synchronization
- Rich, Modern UI with Custom Controls
- Comprehensive Task Management
- Category Organization
- Real-time Updates
- Offline Support

## Screens

1. **Login/Register**
   - User authentication
   - Account creation
   - Password recovery

2. **Task Dashboard**
   - Overview of tasks
   - Quick statistics
   - Priority indicators
   - Progress tracking

3. **Task List View**
   - Filterable task list
   - Sort by various criteria
   - Search functionality
   - Category grouping

4. **Task Detail View**
   - Comprehensive task information
   - Status updates
   - Attachments
   - Comments/Notes

5. **Task Creation/Edit**
   - Rich task editor
   - Category assignment
   - Priority setting
   - Due date management

6. **Categories Management**
   - Create/Edit categories
   - Color coding
   - Category statistics

7. **User Profile**
   - Profile management
   - Preferences
   - Activity history

8. **Settings**
   - App configuration
   - Theme selection
   - Notification preferences
   - Sync settings

## Architecture

### MVVM Implementation
- Base ViewModel for common functionality
- Two-way data binding
- Command pattern implementation
- Property change notifications

### Design Patterns
1. **Repository Pattern**
   - Data access abstraction
   - Consistent interface for data operations

2. **Factory Pattern**
   - Task creation
   - ViewModels instantiation

3. **Observer Pattern**
   - Real-time updates
   - UI synchronization

4. **Strategy Pattern**
   - Task sorting
   - Filtering mechanisms

### Project Structure
```
SmartTaskManager/
├── Models/          # Data models
├── ViewModels/      # MVVM ViewModels
├── Views/           # XAML Views
├── Services/        # Business logic
├── Interfaces/      # Contracts
├── Converters/      # Value converters
├── Helpers/         # Utility classes
└── Resources/       # App resources
```

## Technical Stack

- .NET MAUI
- C# 12
- Azure for backend services
- SQLite for local storage
- Entity Framework Core
- Authentication with Azure AD B2C

## Development Setup

1. Install Visual Studio 2022
2. Install .NET MAUI workload
3. Clone repository
4. Restore NuGet packages
5. Configure Azure services
6. Run the application

## Contributing

1. Create a feature branch from development
2. Implement changes
3. Submit pull request
4. Code review
5. Merge to development

## License

MIT License