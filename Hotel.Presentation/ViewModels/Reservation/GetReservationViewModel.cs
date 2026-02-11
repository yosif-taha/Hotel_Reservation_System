using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.ViewModels.Reservation
{
    public class GetReservationViewModel
    {
        public Guid Id { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string Status { get; set; }
        public Decimal TotalPrice { get; set; }
        public List<Guid> RoomIds { get; set; } = new();
        public Guid UserId { get; set; }
    }
}
