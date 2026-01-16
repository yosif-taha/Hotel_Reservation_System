using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public int Rating { get; set; }     
        public string Comment { get; set; }    
        public DateTime CreatedAt { get; set; }
        public string? StaffResponse { get; set; }

        public Guid UserId { get; set; } //FK
        public User User { get; set; } //Navigation Property
        public Guid RoomId { get; set; } //FK
        public Room Room { get; set; } //Navigation Property
    }
}
