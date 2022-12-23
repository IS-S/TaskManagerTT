using TaskManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccessLayer.Configurations;
using TaskManager.Domain.Commons;

namespace TaskManager.DataAccessLayer
{
    public class TaskManagerDBContext : DbContext // Setting DB context
    {
        public TaskManagerDBContext(DbContextOptions<TaskManagerDBContext> options) : base(options) 
        {
            //If database does not exist, create
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Used when creating objects to be seeded
            var date = DateTime.Now;

            // Build Project Entities.
            modelBuilder.ApplyConfiguration<SiProject>(new ConfigSiProject());
            modelBuilder.Entity<SiProject>().Navigation(x => x.Tasks).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Entity<SiProject>().ToTable("Projects", "dbo")
                // If Projects table is empty, seed sample data
                .HasData(
                new SiProject(1) { StartDate = date, CompletionDate = date.AddDays(200.0), Name = "Project One", Priority = 0, Status = SiEnumProjectStatus.NotStarted },
                new SiProject(2) { StartDate = date.AddDays(-10.0), CompletionDate = date.AddDays(150.0), Name = "Project Two", Priority = 2, Status = SiEnumProjectStatus.Active },
                new SiProject(3) { StartDate = date.AddDays(-5.0), CompletionDate = date.AddDays(100.0), Name = "Project Three", Priority = 1, Status = SiEnumProjectStatus.Active },
                new SiProject(4) { StartDate = date.AddDays(-7.0), CompletionDate = date.AddDays(90.0), Name = "Project Four", Priority = 1, Status = SiEnumProjectStatus.Active },
                new SiProject(5) { StartDate = date.AddDays(-20.0), CompletionDate = date.AddDays(30.0), Name = "Project Five", Priority = 2, Status = SiEnumProjectStatus.Completed }
                );

            // Build Tasks Entities.
            modelBuilder.ApplyConfiguration<SiTask>(new ConfigSiTask());
            modelBuilder.Entity<SiTask>().Navigation(x => x.Project).UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<SiTask>().ToTable("Tasks", "dbo")
            // If Tasks table is empty, seed sample data
                .HasData(
                new SiTask(1) { ProjectId = 1, Description = "Some Desc One", Priority = 0, Name = "Task One", Status = SiEnumTaskStatus.ToDo},
                new SiTask(2) { ProjectId = 1, Description = "Some Desc Two", Priority = 0, Name = "Task Two", Status = SiEnumTaskStatus.InProgress },
                new SiTask(3) { ProjectId = 2, Description = "Some Desc Three", Priority = 0, Name = "Task Three", Status = SiEnumTaskStatus.InProgress },
                new SiTask(4) { ProjectId = 2, Description = "Some Desc Four", Priority = 0, Name = "Task Four", Status = SiEnumTaskStatus.Done },
                new SiTask(5) { ProjectId = 2, Description = "Some Desc Five", Priority = 0, Name = "Task Five", Status = SiEnumTaskStatus.InProgress },
                new SiTask(6) { ProjectId = 3, Description = "Some Desc Six", Priority = 0, Name = "Task Six", Status = SiEnumTaskStatus.ToDo },
                new SiTask(7) { ProjectId = 4, Description = "Some Desc Seven", Priority = 0, Name = "Task Seven", Status = SiEnumTaskStatus.ToDo },
                new SiTask(8) { ProjectId = 4, Description = "Some Desc Eight", Priority = 0, Name = "Task Eight", Status = SiEnumTaskStatus.InProgress },
                new SiTask(9) { ProjectId = 5, Description = "Some Desc Nine", Priority = 0, Name = "Task Nine", Status = SiEnumTaskStatus.Done },
                new SiTask(10) { ProjectId = 5, Description = "Some Desc Ten", Priority = 0, Name = "Task Ten", Status = SiEnumTaskStatus.InProgress});

        }
        public DbSet<SiTask> Tasks { get; set; }
        public DbSet<SiProject> Projects { get; set; }
    }
}
