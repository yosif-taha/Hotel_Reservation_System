using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid UserId { get; set; } //FK
        public User User { get; set; } //Navigation Property
        public Payment Payment { get; set; } //Navigation Property //One to One
        public Invoice Invoice { get; set; } //Navigation Property //One to One
        public ICollection<ReservationRoom> ReservationRooms { get; set; } //Navigation Property



    }
}
