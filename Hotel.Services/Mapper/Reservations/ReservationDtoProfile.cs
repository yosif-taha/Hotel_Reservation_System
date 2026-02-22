using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Reservation;
using System.Reflection;
using System.Security;

namespace Hotel.Services.Mapper.Reservations
{
    public class ReservationDtoProfile : Profile
    {
        public ReservationDtoProfile()
        {
            CreateMap<Reservation, GetByIdReservationDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(des => des.CheckInDate, opt => opt.MapFrom(src => src.CheckInDate))
                .ForMember(des => des.CheckOutDate, opt => opt.MapFrom(src => src.CheckOutDate))
                .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(des => des.RoomIds, opt => opt.MapFrom(scr => scr.ReservationRooms.Select(r => r.RoomId)));

            CreateMap<Reservation, GetReservationsResponseDto>()
                .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.CheckInDate))
                .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => src.CheckOutDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(des => des.RoomIds, opt => opt.MapFrom(scr => scr.ReservationRooms.Select(r => r.RoomId)));

            CreateMap<AddReservationDto, Reservation>()
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ReservationRooms,opt => opt.MapFrom(src =>src.RoomIds.Select(roomID =>new ReservationRoom { RoomId = roomID, NumberOfNights=src.StayDays}).ToList()));
           
        }
    }
}
