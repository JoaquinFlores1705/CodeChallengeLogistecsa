using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Project : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ProjectStartDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ProjectFinishDate { get; set; }
    }
}
