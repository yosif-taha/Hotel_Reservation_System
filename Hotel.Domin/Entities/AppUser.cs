using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();//Navigation Property
        public ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();//Navigation Property
    }
}
