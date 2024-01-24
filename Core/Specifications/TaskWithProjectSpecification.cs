using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Specifications
{
    public class TaskWithProjectSpecification : BaseSpecification<Task>
    {
        public TaskWithProjectSpecification() : base()
        {
            AddInclude(p => p.Project);
        }

        public TaskWithProjectSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Project);
        }
    }
}
