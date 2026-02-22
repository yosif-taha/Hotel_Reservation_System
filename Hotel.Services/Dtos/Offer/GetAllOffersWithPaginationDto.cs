using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Offer
{
    public class GetAllOffersWithPaginationDto
    {
        //Filter criteria
        public string? Title { get; set; }
        public bool? IsActive { get; set; }

        //Pagination properties
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;


    }
}
