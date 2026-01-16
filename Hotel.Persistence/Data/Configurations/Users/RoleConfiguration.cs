using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Data.Configurations.Users
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>

    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
           builder.Property(r => r.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(r => r.IsDeleted)
                  .HasDefaultValue(false);
        }
    }
}
