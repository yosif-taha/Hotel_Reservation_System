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
            builder.Property(f => f.IsDeleted)
                  .HasDefaultValue(false);

            builder.Property(f => f.Rating)
                   .IsRequired();

            builder.Property(f => f.Comment)
                    .HasMaxLength(1000);

            builder.Property(f => f.CreatedAt)
                   .IsRequired();

            builder.Property(f => f.StaffResponse)
                   .HasMaxLength(1000);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "CK_Feedback_Rating_Range",
                    "[Rating] >= 1 AND [Rating] <= 5"
                );
            });

        }
    }
}
