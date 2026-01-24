using AutoMapper;
using Hotel.Presentation.ViewModels.Rooms;
using Hotel.Services.Dtos.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Mapper.Rooms
{
    public class RoomViewModelProfile : Profile
    {
        public RoomViewModelProfile()
        {
            CreateMap<GetRoomResponseDto, GetAllRoomsResponseViewModel>();
            CreateMap<GetRoomResponseDto, GetRoomViewModel>();
            CreateMap<GetAllRoomsWithPaginationViewModel, GetAllRoomsWithPaginationDto>();
            CreateMap<AddRoomViewModel, AddRoomDto>();
            CreateMap<UpdateRoomViewModel, UpdateRoomDto>();

        }
    }
}
