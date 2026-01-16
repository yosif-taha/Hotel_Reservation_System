using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class RoomImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public Guid RoomId { get; set; } //FK
        public Room Room { get; set; } //Navigation Property

    }
}
