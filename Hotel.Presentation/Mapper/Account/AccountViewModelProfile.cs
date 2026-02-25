using AutoMapper;
using Hotel.Presentation.ViewModels.Account;
using Hotel.Services.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Mapper.Account
{
    public class AccountViewModelProfile : Profile
    {
        public AccountViewModelProfile()
        {
            CreateMap<LoginRequestViewModel,LoginRequestDto>();
            CreateMap<RegisterRequestViewModel,RegisterRequestDto>();
            CreateMap<UserResponseDto,UserResponseViewModel>();
        }
    }
}
