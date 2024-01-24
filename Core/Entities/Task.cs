using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Task: BaseClass
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public DateTimeOffset deadline { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
