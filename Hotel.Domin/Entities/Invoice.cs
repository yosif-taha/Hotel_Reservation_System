using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; } 
        public decimal Amount { get; set; }       
        public DateTime IssuedAt { get; set; }   
        public Guid ReservationId { get; set; } //FK 
        public Reservation Reservation { get; set; } // Navigation Property
    }
}
