using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Offers
{
    public class GetAllOffersWithPaginationDto
    {
        //Filter criteria
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool? IsActive { get; set; }
        //Pagination properties
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}
