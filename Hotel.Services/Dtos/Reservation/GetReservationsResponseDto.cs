using Hotel.Domin.Entities.Enums;
using Hotel.Services.Dtos.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Reservation
{
    public class GetReservationsResponseDto
    {
        public Guid Id { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public Decimal TotalPrice { get; set; }
        public List<Guid> RoomIds { get; set; } = new();
        public Guid UserId { get; set; }

    }
}
