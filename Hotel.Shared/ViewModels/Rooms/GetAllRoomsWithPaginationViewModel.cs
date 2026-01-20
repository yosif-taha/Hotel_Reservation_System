using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Shared.ViewModels.Rooms
{
    public class GetAllRoomsWithPaginationViewModel
    {
        //Filter criteria
        public int? RoomNumber { get; set; }
        public decimal? PricePerNight { get; set; }

        //Pagination properties
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
