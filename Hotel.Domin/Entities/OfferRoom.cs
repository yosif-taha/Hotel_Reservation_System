using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class OfferRoom : BaseEntity
    {
        public Guid RoomId { get; set; } //FK
        public Room Room { get; set; } //Navigation Property
        public Guid OfferId { get; set; } //FK
        public Offer Offer { get; set; } //Navigation Property
    }
}
