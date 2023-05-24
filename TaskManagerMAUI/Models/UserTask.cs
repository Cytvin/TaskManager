using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagerMAUI.Models
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsDone { get; set; }
    }
}
