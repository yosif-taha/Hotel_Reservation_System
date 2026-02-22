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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(r => r.CheckInDate)
                .HasConversion(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    d => DateOnly.FromDateTime(d)
                )
                .HasColumnType("date")
                .IsRequired();

            builder.Property(r => r.CheckOutDate)
                .HasConversion(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    d => DateOnly.FromDateTime(d)
                )
                .HasColumnType("date")
                .IsRequired();

            builder.Property(r => r.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Payment)
                .WithOne(p => p.Reservation)
                .HasForeignKey<Payment>(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Invoice)
                .WithOne(i => i.Reservation)
                .HasForeignKey<Invoice>(i => i.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.ReservationRooms)
                .WithOne(rr => rr.Reservation)
                .HasForeignKey(rr => rr.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
