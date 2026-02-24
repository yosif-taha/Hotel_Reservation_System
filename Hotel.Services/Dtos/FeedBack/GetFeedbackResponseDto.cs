using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.FeedBack
{
    public class GetFeedbackResponseDto
    {
        public int? Rating { get; set; }

        public string Comment { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? StaffResponse { get; set; }

        public DateTime? StaffResponseAt { get; set; }

        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public Guid ReservationId { get; set; }

    }
}
