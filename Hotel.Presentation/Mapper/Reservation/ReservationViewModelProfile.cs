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

        public ReservationViewModelProfile()
        {
        
            CreateMap<GetByIdReservationDTO, GetReservationViewModel>();
            CreateMap<AddReservationViewModel, AddReservationDto>();
            //.ForMember(des => des.UserName, opt => opt.MapFrom(scr => _userService.GetUserNameByUserId(scr.UserId)));
            CreateMap<GetAllReservationsWithPaginationViewModel, GetAllReservationsWithPaginationDto>();
            CreateMap<GetReservationsResponseDto, GetReservationsResponseViewModel>();

        }
    }
}
