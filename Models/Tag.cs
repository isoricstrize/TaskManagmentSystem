
using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystem.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 Characters!")]
        public string Name { get; set; } = string.Empty;
    }
}