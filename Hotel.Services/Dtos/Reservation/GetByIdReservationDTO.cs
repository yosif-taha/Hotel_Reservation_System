using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Reservation
{
    public class GetByIdReservationDTO
    {
        public Guid Id { get; set; }
        public DateTime CheckedInDate { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public Decimal TotalPrice { get; set; }
        public List<Guid> RoomIds { get; set; } = new();
        public Guid UserId { get; set; }
    }
}
