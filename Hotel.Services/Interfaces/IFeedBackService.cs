using Hotel.Services.Dtos.FeedBack;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IFeedBackService
    {
        public Task<ResultT<IEnumerable<GetFeedbackResponseDto>>> GetAllFeedbackAsync(GetAllFeedbackWithPaginationDto getAllFeedbackWithPaginationDto);
        public Task<ResultT<GetFeedbackResponseDto>> GetFeedbackByIdAsync(Guid feedbackId);
        public Task<Result> AddFeedBackAsync(AddFeedBackDto addFeedBackDto, Guid userId);
        public Task<Result> DeleteFeedBackAsync(Guid feedbackId, Guid userId);
    }
}
