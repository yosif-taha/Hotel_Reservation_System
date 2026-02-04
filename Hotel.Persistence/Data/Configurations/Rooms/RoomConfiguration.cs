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
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
           builder.Property(r => r.PricePerNight)
                  .HasColumnType("decimal(18,2)");
            builder.Property(r => r.Description)
                   .HasMaxLength(1000);
            //builder.Property(r => r.IsAvailable).HasDefaultValue(true); // ==> Availability Is Determined By ReservationRooms In Different Time
            builder.Property(r => r.IsDeleted)
                   .HasDefaultValue(false);


            builder.HasOne(r => r.RoomType)
                   .WithMany(rt => rt.Rooms)
                   .HasForeignKey(r => r.RoomTypeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Images)
                   .WithOne(ri => ri.Room)
                   .HasForeignKey(ri => ri.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Facilities)
                   .WithOne(rf => rf.Room)
                   .HasForeignKey(rf => rf.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.OfferRooms)
                   .WithOne(or => or.Room)
                   .HasForeignKey(or => or.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Feedbacks)
                   .WithOne(f => f.Room)
                   .HasForeignKey(f => f.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.ReservationRooms)
                   .WithOne(rr => rr.Room)
                   .HasForeignKey(rr => rr.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
