using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.Models;
using Task = TaskManagmentSystem.Models.Task;

namespace TaskManagmentSystem.Data
{
    public class TaskManagmentContext : DbContext
    {
        public TaskManagmentContext(DbContextOptions<TaskManagmentContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskDetail> TaskDetails { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(e => e.TaskDetail)
                .WithOne(e => e.Task)
                .HasForeignKey<TaskDetail>(e => e.TaskId)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.Tasks)
                .WithMany(e => e.Tags);
        }            
    }
}