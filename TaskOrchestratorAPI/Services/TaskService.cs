using TaskOrchestratorAPI.Model;

namespace TaskOrchestratorAPI.Services;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _items = new();

    public IEnumerable<TaskItem> GetAll()
    {
        return _items;
    }

    public TaskItem? GetById(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public TaskItem Add(TaskItem item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);

        return item;
    }
}