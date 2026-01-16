using Hotel.Domain.Entities.Enums;
using Hotel.Domin.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaidAt { get; set; }
        public Guid ReservationId { get; set; } //FK
        public Reservation Reservation { get; set; } //Navigation Property

    }
}
