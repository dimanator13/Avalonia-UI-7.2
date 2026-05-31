using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task_2.Models;
using Task_2.Services;

namespace Task_2.ViewModels;

public partial class TasksViewModel : ViewModelBase
{
    private readonly ITaskService _taskService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasSelectedTask))]
    [NotifyCanExecuteChangedFor(nameof(DeleteTaskCommand))]
    [NotifyCanExecuteChangedFor(nameof(MarkAsDoneCommand))]
    private TaskItemViewModel? _selectedTask;

    [ObservableProperty]
    private string _newTaskTitle = string.Empty;

    public bool HasSelectedTask => SelectedTask is not null;

    public ObservableCollection<TaskItemViewModel> Tasks { get; } = new();

    public ObservableCollection<TaskPriority> AvailablePriorities { get; } = new()
    {
        TaskPriority.Low,
        TaskPriority.Normal,
        TaskPriority.High
    };

    public TasksViewModel(ITaskService taskService)
    {
        _taskService = taskService;

        foreach (var task in _taskService.GetTasks())
        {
            Tasks.Add(new TaskItemViewModel(task));
        }
    }

    [RelayCommand]
    private void AddTask()
    {
        if (string.IsNullOrWhiteSpace(NewTaskTitle))
            return;

        var task = _taskService.CreateTask(NewTaskTitle.Trim());

        var taskViewModel = new TaskItemViewModel(task);

        Tasks.Add(taskViewModel);

        SelectedTask = taskViewModel;
        NewTaskTitle = string.Empty;
    }

    [RelayCommand(CanExecute = nameof(HasSelectedTask))]
    private void DeleteTask()
    {
        if (SelectedTask is null)
            return;

        _taskService.DeleteTask(SelectedTask.Model);

        Tasks.Remove(SelectedTask);
        SelectedTask = null;
    }

    [RelayCommand(CanExecute = nameof(HasSelectedTask))]
    private void MarkAsDone()
    {
        if (SelectedTask is null)
            return;

        _taskService.MarkAsDone(SelectedTask.Model);

        SelectedTask.RefreshState();
    }
}