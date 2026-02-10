//using AutoMapper;
//using Hotel.Domain.Entities;
//using Hotel.Services.Dtos.Offer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hotel.Services.Mapper.Offers
//{
//    public class OfferDtoProfile:Profile
//    {
//        OfferDtoProfile() {
//            CreateMap<Offer, GetOfferResponseDto>().ForMember(d => d.RoomIds,opt => opt.MapFrom(s => s.OfferRooms.Select(or => or.RoomId)));

//            CreateMap<AddOfferDto, Offer>().ForMember(d => d.OfferRooms, opt => opt.Ignore());

//            CreateMap<UpdateOfferDto, Offer>().ForAllMembers(opt => opt.Condition((src, dest, val) => val != null));


//        }
//    }
//}
