using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.ViewModels.FeedBack
{
    public class AddFeedBackViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public Guid RoomId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
