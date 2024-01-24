using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Client:BaseClass
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
