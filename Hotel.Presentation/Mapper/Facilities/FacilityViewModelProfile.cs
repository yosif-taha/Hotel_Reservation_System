using AutoMapper;
using Hotel.Presentation.ViewModels.Facility;
using Hotel.Services.Dtos.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Mapper.Facilities
{
    public class FacilityViewModelProfile:Profile
    {
        public FacilityViewModelProfile() {
            CreateMap<GetAllFacilitiesWithPaginationViewModel, GetAllFacilitiesWithPaginationDto>();
            CreateMap<GetFacilityResponseDto, GetFacilityResponseViewModel>();
            CreateMap<AddFacilityViewModel, AddFacilityDto>();
            CreateMap<UpdateFacilityViewModel, UpdateFacilityDto>();
        }
    }
}


