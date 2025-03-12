
using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystem.Models
{
    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }

    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "Task name must be between 3 and 30 Characters!")]
        public string Name { get; set; } = string.Empty;

        public Status Status { get; set; } = Status.Pending;


        // Relationship with TaskDetail (One-to-One) - Principal (Parent)
        public TaskDetail? TaskDetail { get; set; }


        // Relationship with User (One-to-Many) - Dependent (child)
        public int? UserId { get; set; }
        public User? User{ get; set; }


        // Relationship with Tag (Many-to-Many)   
        public ICollection<Tag> Tags { get; set; } = [];
    }
}