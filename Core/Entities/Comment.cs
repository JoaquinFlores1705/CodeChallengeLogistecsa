using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Comment:BaseClass
    {
        public string Content { get; set; }
        public DateTimeOffset Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
