using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Facility
{
    public class GetFacilityResponseDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? IconURL { get; set; }
        public List<Guid> RoomIds { get; set; } = new List<Guid>();
    }
}
