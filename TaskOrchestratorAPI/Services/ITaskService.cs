using TaskOrchestratorAPI.Model;

namespace TaskOrchestratorAPI.Services;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Add(TaskItem item);
}