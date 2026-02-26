using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Facility;
using Hotel.Services.Dtos.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Mapper.FeedBack
{
    public class FeedBackDtoProfile : Profile
    {
        public FeedBackDtoProfile()
        {
            CreateMap<AddFeedBackDto, Feedback>();
            CreateMap<Feedback, GetFeedbackResponseDto>();
        }
    }
}
