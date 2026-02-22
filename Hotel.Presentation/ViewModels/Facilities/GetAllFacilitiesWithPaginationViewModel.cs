using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.ViewModels.Facility
{
    public class GetAllFacilitiesWithPaginationViewModel
    {
        //Filter criteria
        public string? Name { get; set; }

        //Pagination properties
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
