using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.ViewModels.Reservation
{
    public class GetAllReservationsWithPaginationViewModel
    {
        //Filter criteria
        public DateOnly? CheckInDate { get; set; }
        //public DateTime CheckOutDate { get; set; }
        public ReservationStatus? Status { get; set; }
        //public decimal TotalPrice { get; set; }

        //Pagination properties
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
