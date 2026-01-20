using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Shared.Dtos.Rooms;
using Hotel.Shared.ViewModels.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Mapper.Rooms
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        {
            CreateMap<Room, GetRoomResponseDto>();
            CreateMap<GetRoomResponseDto, GetAllRoomsResponseViewModel>();
            CreateMap<GetRoomResponseDto, GetRoomViewModel>();
            CreateMap<GetAllRoomsWithPaginationViewModel,GetAllRoomsWithPaginationDto>();
            CreateMap<AddRoomViewModel, AddRoomDto>();
            CreateMap<AddRoomDto, Room>();
            CreateMap<Room, GetRoomResponseDto>();
            CreateMap<UpdateRoomViewModel, UpdateRoomDto>();
            CreateMap<UpdateRoomDto, Room>();
        }
    }
}
