using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class AttendanceConfig : IEntityTypeConfiguration<Attendance>
    {
       

        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .HasConversion<string>();

            builder.HasOne(x => x.Student)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(x => x.Lesson)
                   .WithMany(l => l.Attendances)
                   .HasForeignKey(x => x.LessonId);
        }
    }
}
