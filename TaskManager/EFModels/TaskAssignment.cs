using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.EFModels
{
    public class TaskAssignment
    {
        [Key]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public bool IsDone { get; set; }
        public User? User { get; set; }
        public UserTask? Task { get; set; }
    }
}
