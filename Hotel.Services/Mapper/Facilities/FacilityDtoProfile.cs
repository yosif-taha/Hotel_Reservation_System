using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Mapper.Facilities
{
    public class FacilityDtoProfile:Profile
    {
        public FacilityDtoProfile() {
            CreateMap<Facility, GetFacilityResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.IconURL, opt => opt.MapFrom(src => src.IconURL))
                .ForMember(dest => dest.RoomIds,opt=>opt.MapFrom(src=>src.RoomFacilities.Select(roomId=> roomId.RoomId)));
            CreateMap<AddFacilityDto, Facility>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.IconURL, opt => opt.MapFrom(src => src.IconURL))
                .ForMember(dest => dest.RoomFacilities,opt=>opt.MapFrom(src=>src.RoomIds.Select(roomId => new ReservationRoom { RoomId=roomId})));
            CreateMap<UpdateFacilityDto, Facility>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.IconURL, opt => opt.MapFrom(src => src.IconURL))
                .ForMember(dest => dest.RoomFacilities, opt => opt.MapFrom(src => src.RoomIds.Select(roomId => new ReservationRoom { RoomId = roomId })));
        }
    }
}
