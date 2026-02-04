using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.ViewModels.Reservation
{
    public class GetReservationsResponseViewModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
