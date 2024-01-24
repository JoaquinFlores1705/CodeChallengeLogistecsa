using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {

        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(250);
            builder.Property(s => s.Status)
                .HasConversion(
                o => o.ToString(),
                o => (TaskStatus)Enum.Parse(typeof(TaskStatus), o)
            );
            builder.Property(p => p.deadline).IsRequired();
            builder.HasOne(p => p.Project).WithMany().HasForeignKey(p => p.ProjectId);
        }
    }
}
