using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Mapper.Rooms
{
    public class RoomDtoProfile : Profile
    {
        public RoomDtoProfile()
        {
            CreateMap<Room, GetRoomResponseDto>();
            CreateMap<AddRoomDto, Room>();
            CreateMap<Room, GetRoomResponseDto>();
            CreateMap<UpdateRoomDto, Room>();
        }
    }
}
