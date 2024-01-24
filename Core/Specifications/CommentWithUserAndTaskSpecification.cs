using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CommentWithUserAndTaskSpecification : BaseSpecification<Comment>
    {
        public CommentWithUserAndTaskSpecification() : base()
        {
            AddInclude(p => p.User);
            AddInclude(p => p.Task);
        }

        public CommentWithUserAndTaskSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.User);
            AddInclude(p => p.Task);
        }
    }
}
