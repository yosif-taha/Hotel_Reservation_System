using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Offer;

namespace Hotel.Services.Mapper.Offers
{
    public class OfferDtoProfile : Profile
    {
       public OfferDtoProfile()
        {
            CreateMap<Offer, GetOfferResponseDto>()
            .ForMember(des=>des.RoomsId,opt=>opt.MapFrom(src => src.OfferRooms.Select(r=>r.RoomId)));
            CreateMap<AddOfferDto, Offer>();

        }
    }
}
