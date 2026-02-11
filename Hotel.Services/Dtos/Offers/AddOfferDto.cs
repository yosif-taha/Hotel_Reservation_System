using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Offers
{
    public class AddOfferDto
    {
        public Guid OfferId { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        //public List<Guid> RoomsId { get; set; } = new List<Guid>(); //???
    }
}
