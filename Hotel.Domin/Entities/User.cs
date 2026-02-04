using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }= null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; } 
        public Guid RoleId { get; set; } //FK
        public Role Role { get; set; } = null!; //Navigtion Property
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();//Navigation Property
        public ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();//Navigation Property

    }
}
