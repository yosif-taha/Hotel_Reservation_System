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
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid UserId { get; set; } //FK
        public User User { get; set; } = null!; //Navigation Property
        public Payment Payment { get; set; } = null!;//Navigation Property //One to One
        public Invoice Invoice { get; set; } = null!;//Navigation Property //One to One
        public ICollection<ReservationRoom> ReservationRooms { get; set; } = new HashSet<ReservationRoom>(); //Navigation Property



    }
}
