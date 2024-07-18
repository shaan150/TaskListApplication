using TaskListApplication.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskListApplication.Server.Data
{
    public class TaskContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }
    }
}
