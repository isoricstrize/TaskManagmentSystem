
using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystem.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        // Relationship with Task (Many-to-Many)   
        public ICollection<Task> Tasks { get; set; } = [];
    }
}