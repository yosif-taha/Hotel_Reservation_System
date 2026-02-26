using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Data.Configurations.Payments
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedbacks", tb =>
            {
                tb.HasCheckConstraint(
                    "CK_Feedback_Rating_Range",
                    "[Rating] >= 1 AND [Rating] <= 5"
                );

            });

            // Each reservation can have only one feedback, and each room can have only one feedback per reservation
            builder.HasIndex(f => new { f.ReservationId, f.RoomId })
            .IsUnique();

            builder.Property(f => f.Rating)
                   .IsRequired();

            builder.Property(f => f.Comment)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(f => f.StaffResponse)
                   .HasMaxLength(1000);

            builder.Property(f => f.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(f => f.IsDeleted)
                   .HasDefaultValue(false);



            builder.HasOne(f => f.Room)
                   .WithMany(r => r.Feedbacks)
                   .HasForeignKey(f => f.RoomId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.User)
                   .WithMany(u => u.Feedbacks)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Reservation)
                   .WithOne()
                   .HasForeignKey<Feedback>(f => f.ReservationId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
