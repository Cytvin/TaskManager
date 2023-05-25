using Microsoft.EntityFrameworkCore;

namespace TaskManagerWeb.EFModels
{
    public class TaskManagerDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        public TaskManagerDBContext(DbContextOptions<TaskManagerDBContext> options) : base(options) { }
    }
}
