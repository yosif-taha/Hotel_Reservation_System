using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Reservation
{
    public class AddReservationDto
    {
        public Guid UserId { get; set; }
        public List<Guid> RoomIds { get; set; } = new();
        public Decimal TotalPrice { get; set; }
        public DateOnly? CheckInDate { get; set; }
        public ReservationStatus Status { get; set; }
        public int StayDays { get; set; } = 5;       
    }
}
