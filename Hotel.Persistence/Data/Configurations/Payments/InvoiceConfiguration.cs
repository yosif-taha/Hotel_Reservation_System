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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.IsDeleted)
                  .HasDefaultValue(false);

            builder.Property(i => i.InvoiceNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(i => i.Amount)
                   .HasColumnType("decimal(18,2)")
                   . IsRequired();

            builder.Property(i => i.IssuedAt)
                   .IsRequired();

        }
    }
}
