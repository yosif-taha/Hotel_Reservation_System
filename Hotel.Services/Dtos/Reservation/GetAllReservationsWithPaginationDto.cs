using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Reservation
{
    public class GetAllReservationsWithPaginationDto
    {
        //Filter criteria
        public DateTime? CheckInFrom { get; set; }
        public DateTime? CheckOutTo { get; set; }
        public ReservationStatus? Status { get; set; }
        public Guid? UserId { get; set; }
        //Pagination properties
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
