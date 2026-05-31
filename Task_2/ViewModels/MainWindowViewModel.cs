using Task_2.Services;

namespace Task_2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public TasksViewModel TasksViewModel { get; }

    public MainWindowViewModel()
    {
        ITaskService taskService = new InMemoryTaskService();

        TasksViewModel = new TasksViewModel(taskService);
    }
}