using Microsoft.EntityFrameworkCore;
using TaskOrchestratorAPI.Model;

namespace TaskOrchestratorAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
}