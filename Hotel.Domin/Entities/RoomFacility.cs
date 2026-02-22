using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class RoomFacility
    {
        public Guid RoomId { get; set; } //FK
        public Room Room { get; set; } = null!;//Navigation Property
        public Guid FacilityId { get; set; } //FK
        public Facility Facility { get; set; } = null!;//Navigation Property
    }
}
