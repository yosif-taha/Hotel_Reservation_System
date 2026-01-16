using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; } 
        public Guid RoleId { get; set; } //FK
        public Role Role { get; set; } //Navigtion Property
        public ICollection<Reservation> Reservations { get; set; } //Navigation Property
        public ICollection<Feedback> Feedbacks { get; set; } //Navigation Property

    }
}
