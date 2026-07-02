using TaskOrchestratorAPI.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskOrchestratorAPI.Model;

public class TaskItem
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; }
}
