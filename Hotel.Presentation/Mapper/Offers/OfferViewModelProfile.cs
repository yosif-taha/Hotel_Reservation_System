using AutoMapper;
using Hotel.Presentation.ViewModels.Offers;
using Hotel.Services.Dtos.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Mapper.Offers
{
    public class OfferViewModelProfile : Profile
    {
        public OfferViewModelProfile()
        {
            CreateMap<AddOfferViewModel, AddOfferDto>();
            CreateMap<GetAllOffersWithPaginationDto, GetAllOffersWithPaginationViewModel>();
            CreateMap<GetAllOffersWithPaginationViewModel, GetAllOffersWithPaginationDto>();
            CreateMap<GetOfferResponseDto, GetAllOffersViewModel>();
            CreateMap<GetOfferResponseDto, GetOfferResponseViewModel>();
            CreateMap<UpdateOfferViewModel, UpdateOfferDto>();
        }
    }
}
