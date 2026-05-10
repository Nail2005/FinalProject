using Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class GuidanceConfig : IEntityTypeConfiguration<Guidance>
    {
        public void Configure(EntityTypeBuilder<Guidance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Notes)
                   .HasMaxLength(500);

            builder.HasOne(x => x.Student)
                   .WithMany(s => s.Guidances)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(x => x.Lesson)
                   .WithMany(l => l.Guidances)
                   .HasForeignKey(x => x.LessonId);
        }
    }
}
