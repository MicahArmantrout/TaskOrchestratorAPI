using TaskOrchestratorAPI.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskOrchestratorAPI.Model;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public string Description { get; set; } = string.Empty; 
    public DateTime CreatedOn { get; set; } 
    public string CreatedBy { get; set; } = string.Empty;   
    public DateTime LastModifiedOn { get; set; }
    public Priority?  Priority { get; set; }
    public TaskStatus?  Status { get; set; } 
}


