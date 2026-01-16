using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Data.Configurations.Rooms
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.Property(rt => rt.Name)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(rt => rt.IsDeleted)
                  .HasDefaultValue(false);

            builder.Property(rt => rt.MaxGuests)
                   .IsRequired();
            builder.ToTable(tb => tb.HasCheckConstraint("CK_RoomType_MaxGuests", "[MaxGuests] > 0"));


        }
    }
}
