
using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 Characters!")]
        public string Name { get; set; } = string.Empty;


        // Relationship with Task (One-to-Many) - Principal (Parent)     
        public ICollection<Task> Tasks { get; set; } = [];
    }
}