using AutoMapper;
using Hotel.Presentation.ViewModels.FeedBack;
using Hotel.Services.Dtos.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Mapper.Feedback
{
    public class FeedbackViewModelProfile : Profile
    {
        public FeedbackViewModelProfile()
        {
            CreateMap<AddFeedBackViewModel, AddFeedBackDto>();
            CreateMap<GetAllFeedbacksWithPaginationViewModel, GetAllFeedbackWithPaginationDto>();
             CreateMap<GetFeedbackResponseDto, GetFeedbackResponseViewModel>();
             CreateMap<AddStaffResponseViewModel, AddStaffResponseDto>();
        }
    }
}
