using Hotel.Services.Dtos.Facility;
using Hotel.Services.Dtos.Offer;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IFacilityService
    {
        Task<ResultT<IEnumerable<GetFacilityResponseDto>>> GetAllFacilities(GetAllFacilitiesWithPaginationDto dto);
        Task<ResultT<GetFacilityResponseDto>> GetFacilityByIdAsync(Guid id);
        Task<Result> AddFacilityAsync(AddFacilityDto createFacilityDto);
        Task<Result> UpdateFacilityAsync(Guid id, UpdateFacilityDto updateFacilityDto);
        Task<Result> DeleteFacilityAsync(Guid id);
        Task<ResultT<IEnumerable<GetFacilityResponseDto>>> GetRoomFacilitiesByRoomIdAsync(Guid roomId);

    }
}
