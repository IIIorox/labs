using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserTask> Tasks { get; set; }
    }
}
