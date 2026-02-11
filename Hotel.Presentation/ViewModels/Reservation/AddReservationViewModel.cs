using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.ViewModels.Reservation
{
    public class AddReservationViewModel
    {
        public Guid UserId { get; set; }
        public List<Guid> RoomIds { get; set; } = new();
        public DateOnly? CheckInDate { get; set; }   // optional
        public int? StayDays { get; set; }           // optional
    }
}
