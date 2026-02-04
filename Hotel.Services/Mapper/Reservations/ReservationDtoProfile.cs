using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Reservation;

namespace Hotel.Services.Mapper.Reservations
{
    public class ReservationDtoProfile : Profile
    {
        public ReservationDtoProfile()
        {
            CreateMap<Reservation, GetByIdReservationDTO>()
                .ForMember(des => des.Id ,opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Status,opt=>opt.MapFrom(src => src.Status))
                .ForMember(des => des.CheckedInDate,opt => opt.MapFrom(src => src.CheckInDate))
                .ForMember(des => des.CheckedOutDate,opt => opt.MapFrom(src => src.CheckOutDate))
                .ForMember(des => des.UserId,opt => opt.MapFrom(src => src.UserId))
                ;

        }
    }
}
