using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; } //Navigation Property
    }
}
