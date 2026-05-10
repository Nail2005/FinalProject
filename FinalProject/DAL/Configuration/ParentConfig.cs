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
    public class ParentConfig : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PhoneNumber)
                   .IsRequired();

            builder.HasIndex(x => x.PhoneNumber)
                   .IsUnique(); 

            builder.HasOne(x => x.User)
                   .WithOne(u => u.Parent)
                   .HasForeignKey<Parent>(x => x.UserId);
        }
    }
}
