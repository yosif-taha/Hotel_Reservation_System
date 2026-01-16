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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.IsDeleted)
                  .HasDefaultValue(false);

            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PaymentMethod)
                    .IsRequired();
                   
            builder.Property(p => p.PaymentStatus)
                   .IsRequired();

            builder.Property(p => p.TransactionId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.PaidAt)
                   .IsRequired();
        }
    }
}
