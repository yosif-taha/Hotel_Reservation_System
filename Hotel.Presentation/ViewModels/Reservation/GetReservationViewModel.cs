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
        public int Id { get; set; }
        public DateTime CheckedInDate { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public int Capacity { get; set; }
        public Decimal TotalPrice { get; set; }
        public int RoomId { get; set; }
        public string? UserName { get; set; }
    }
}
