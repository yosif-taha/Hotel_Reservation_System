using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Room : BaseEntity
    {
        public int RoomNumber { get; set; } 
        public decimal PricePerNight { get; set; }
        //public bool IsAvailable { get; set; } // ==> Availability Is Determined By ReservationRooms In Different Time 
        public string? Description { get; set; }
        public Guid RoomTypeId { get; set; } //FK
        public RoomType RoomType { get; set; } = null!;//Navigation Property 
        public ICollection<RoomImage> Images { get; set; } = new HashSet<RoomImage>();//Navigation Property
        public ICollection<RoomFacility> Facilities { get; set; } = new HashSet<RoomFacility>(); //Navigation Property
        public ICollection<OfferRoom> OfferRooms { get; set; } = new HashSet<OfferRoom>();//Navigation Property
        public ICollection<Feedback> Feedbacks { get; set; } =new HashSet<Feedback>(); //Navigation Property
        public ICollection<ReservationRoom> ReservationRooms { get; set; } = new HashSet<ReservationRoom>(); //Navigation Property //In Different Time


    }
}
