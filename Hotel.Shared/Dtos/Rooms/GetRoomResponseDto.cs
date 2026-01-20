using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Shared.Dtos.Rooms
{
    public class GetRoomResponseDto
    {
        public int RoomNumber { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
     
    }
}
