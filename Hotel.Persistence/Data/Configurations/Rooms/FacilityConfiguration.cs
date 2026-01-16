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
    public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.Property(F => F.Name)
                   .HasMaxLength(100);
            builder.Property(f => f.IsDeleted)
                  .HasDefaultValue(false);

            builder.HasMany(F => F.RoomFacilities)
                   .WithOne(RF => RF.Facility)
                   .HasForeignKey(RF => RF.FacilityId)
                     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
