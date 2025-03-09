
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagmentSystem.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string Description { get; set; } = string.Empty;


        // One-to-One Relationship with Task - Dependent (child)     
        public int? TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Task? Task{ get; set; }
    }
}