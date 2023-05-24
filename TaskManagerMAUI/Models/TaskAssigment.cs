using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerMAUI.Models
{
    public class TaskAssigment
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public bool IsDone { get; set; }
    }
}
