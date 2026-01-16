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
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.Property(o => o.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(o => o.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(o => o.StartDate)
                   .IsRequired();

            builder.Property(o => o.EndDate)
                   .IsRequired();

            builder.Property(o => o.DiscountPercentage)
                   .IsRequired()
                   .HasColumnType("decimal(5, 2)");

            builder.Property(o => o.IsActive)
                   .HasDefaultValue(false);

            builder.ToTable(tb =>
                              tb.HasCheckConstraint("CK_Reservation_StartDate", "[StartDate] >= GETDATE()")
                              );

            builder.HasMany(o => o.OfferRooms)
                   .WithOne(or => or.Offer)
                   .HasForeignKey(or => or.OfferId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
