using System.Collections.Generic;
using Task_2.Models;

namespace Task_2.Services;

public interface ITaskService
{
    IEnumerable<TaskItem> GetTasks();

    TaskItem CreateTask(string title);

    void DeleteTask(TaskItem task);

    void MarkAsDone(TaskItem task);
}