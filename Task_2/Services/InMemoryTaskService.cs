using System;
using System.Collections.Generic;
using Task_2.Models;

namespace Task_2.Services;

public class InMemoryTaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new()
    {
        new TaskItem(
            "Выучить MVVM",
            "Разобраться с Model, ItemViewModel и Service",
            false,
            TaskPriority.High,
            DateTimeOffset.Now.AddDays(1)),

        new TaskItem(
            "Сделать практику",
            "Переделать редактор задач на более взрослую структуру",
            false,
            TaskPriority.Normal,
            DateTimeOffset.Now.AddDays(-1)),

        new TaskItem(
            "Повторить Binding",
            "SelectedItem, DataTemplate, ObservableCollection",
            true,
            TaskPriority.Low,
            DateTimeOffset.Now)
    };

    public IEnumerable<TaskItem> GetTasks()
    {
        return _tasks;
    }

    public TaskItem CreateTask(string title)
    {
        var task = new TaskItem(
            title,
            string.Empty,
            false,
            TaskPriority.Normal,
            DateTimeOffset.Now.AddDays(1));

        _tasks.Add(task);

        return task;
    }

    public void DeleteTask(TaskItem task)
    {
        _tasks.Remove(task);
    }

    public void MarkAsDone(TaskItem task)
    {
        task.IsDone = true;
    }
}