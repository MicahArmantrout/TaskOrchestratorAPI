using TaskOrchestratorAPI.Data;
using TaskOrchestratorAPI.Model;

namespace TaskOrchestratorAPI.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<TaskItem> GetAllByUserId(string userId)
    {
        return _db.TaskItems
            .Where(x => x.UserId == userId)
            .ToList();
    }

    public TaskItem? GetById(int id, string userId)
    {
        return _db.TaskItems
            .FirstOrDefault(x => x.Id == id && x.UserId == userId);
    }

    public TaskItem Add(TaskItem item)
    {
        _db.TaskItems.Add(item);
        _db.SaveChanges();

        return item;
    }

    public TaskItem? Update(int id, string userId, TaskItem item)
    {
        var existingItem = _db.TaskItems
            .FirstOrDefault(x => x.Id == id && x.UserId == userId);

        if (existingItem == null)
            return null;

        existingItem.Title = item.Title;
        existingItem.IsComplete = item.IsComplete;
        existingItem.Description = item.Description;
        existingItem.Status = item.Status;

        _db.SaveChanges();

        return existingItem;
    }

    public bool Delete(int id, string userId)
    {
        var existingItem = _db.TaskItems
            .FirstOrDefault(x => x.Id == id && x.UserId == userId);

        if (existingItem == null)
            return false;

        _db.TaskItems.Remove(existingItem);
        _db.SaveChanges();

        return true;
    }
}
