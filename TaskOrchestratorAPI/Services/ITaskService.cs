using TaskOrchestratorAPI.Model;

namespace TaskOrchestratorAPI.Services;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAllByUserId(string userId);
    TaskItem? GetById(int id, string userId);
    TaskItem Add(TaskItem item);
    TaskItem? Update(int id, string userId, TaskItem item);
    bool Delete(int id, string userId);
}
