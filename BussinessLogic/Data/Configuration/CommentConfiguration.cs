using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(p => p.Content).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Date).IsRequired();
            builder.HasOne(u => u.User).WithMany().HasForeignKey(u => u.UserId);
            builder.HasOne(t => t.Task).WithMany().HasForeignKey(t => t.TaskId);
        }
    }
}
