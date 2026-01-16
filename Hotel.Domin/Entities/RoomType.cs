using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class RoomType : BaseEntity
    {
        public string Name { get; set; }
        public int MaxGuests { get; set; }
        public ICollection<Room> Rooms { get; set; }
 
    }
}
