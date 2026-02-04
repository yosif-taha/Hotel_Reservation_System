using AutoMapper;
using Hotel.Presentation.ViewModels.Reservation;
using Hotel.Services.Dtos.Reservation;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Mapper.Reservation
{
    public class ReservationViewModelProfile : Profile
    {
        private readonly IUserService _userService;

        public ReservationViewModelProfile(IUserService userService)
        {
            _userService = userService;
            CreateMap<GetByIdReservationDTO, GetReservationViewModel>()
                .ForMember(des => des.UserName, opt => opt.MapFrom(scr => _userService.GetUserNameByUserId(scr.UserId)));
        }
    }
}
