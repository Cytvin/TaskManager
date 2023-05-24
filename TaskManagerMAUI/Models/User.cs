using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagerMAUI.Models
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
