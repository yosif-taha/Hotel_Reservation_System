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
    public class OfferRoomConfiguration : IEntityTypeConfiguration<OfferRoom>
    {
        public void Configure(EntityTypeBuilder<OfferRoom> builder)
        {
            builder.HasKey(or => new { or.OfferId, or.RoomId });
        }
    }
}
