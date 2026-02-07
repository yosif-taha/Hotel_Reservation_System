using AutoMapper;
using Hotel.Presentation.ViewModels.Offer;
using Hotel.Services.Dtos.Offers;
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
        }
    }
}
