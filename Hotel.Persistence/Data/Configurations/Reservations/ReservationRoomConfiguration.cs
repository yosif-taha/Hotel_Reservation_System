using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Data.Configurations.Reservations
{
    public class ReservationRoomConfiguration : IEntityTypeConfiguration<ReservationRoom>
    {
        public void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {   
            builder.HasKey(rr => new { rr.ReservationId, rr.RoomId });

            builder.Property(rr => rr.PricePerNight)
                   .HasColumnType("decimal(18,2)");

            builder.Property(rr => rr.NumberOfNights)
                   .IsRequired();
            builder.ToTable(tb => tb.HasCheckConstraint("CK_Reservation_NumberOfNights", "[NumberOfNights] > 0"));


        }
    }
}
