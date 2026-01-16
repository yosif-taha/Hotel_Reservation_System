using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class ReservationRoom
    {
        public Guid ReservationId { get; set; } //FK
        public Reservation Reservation { get; set; } //Navigation Property
        public Guid RoomId { get; set; } //FK
        public Room Room { get; set; } //Navigation Property
        public decimal PricePerNight { get; set; } 
        public int NumberOfNights { get; set; }
    }
}
