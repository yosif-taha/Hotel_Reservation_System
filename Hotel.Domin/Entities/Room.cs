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
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public Guid RoomTypeId { get; set; } //FK
        public RoomType RoomType { get; set; } //Navigation Property 
        public ICollection<RoomImage> Images { get; set; } //Navigation Property
        public ICollection<RoomFacility> Facilities { get; set; } //Navigation Property
        public ICollection<OfferRoom> OfferRooms { get; set; } //Navigation Property
        public ICollection<Feedback> Feedbacks { get; set; } //Navigation Property
        public ICollection<ReservationRoom> ReservationRooms { get; set; } //Navigation Property //In Different Time


    }
}
