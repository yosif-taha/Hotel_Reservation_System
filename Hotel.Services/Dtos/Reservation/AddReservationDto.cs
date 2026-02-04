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
        public DateTime? CheckInDate { get; set; }   // optional
        public int? StayDays { get; set; }           // optional
    }
}
