using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.FeedBack
{
    public class GetAllFeedbackWithPaginationDto
    {
        //Filter criteria
        public int? Rating { get; set; } 


        //Pagination properties
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
