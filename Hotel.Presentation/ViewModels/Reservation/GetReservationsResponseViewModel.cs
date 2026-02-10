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
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
