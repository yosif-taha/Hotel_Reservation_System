using AutoMapper;
using FluentValidation;
using Hotel.Domain.Entities;
using Hotel.Presentation.Helpers;
using Hotel.Presentation.Validations.Facilities;
using Hotel.Presentation.ViewModels.Facility;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.Dtos.Facility;
using Hotel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FacilityController(IFacilityService _facilityService , IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseViewModel> GetAllFacilities([FromQuery] GetAllFacilitiesWithPaginationViewModel model)
        {
            var validator = new GetAllFacilitiesWithPaginationViewModelValidator().Validate(model);
            if (!validator.IsValid)
                return new FailedResponseViewModel(ErrorType.InvalidFacilityData, "Invalid Facility Data From Request");
            var requestDto = _mapper.Map<GetAllFacilitiesWithPaginationDto>(model);
            var serviceResult = await _facilityService.GetAllFacilities(requestDto);
            var items = _mapper.Map<IEnumerable<GetFacilityResponseViewModel>>(serviceResult.Data);
            int totalCount = items.Count();

            var pagedResult = new PagedResult<GetFacilityResponseViewModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
            };

            return new SuccessResponseViewModelT<PagedResult<GetFacilityResponseViewModel>>(pagedResult);
        }

        [HttpGet]
        public async Task<ResponseViewModel> GetFacility(Guid id)
        {
            var result = await _facilityService.GetFacilityByIdAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.FacilityNotFound, "Facility not found !");
            var data = _mapper.Map<GetFacilityResponseViewModel>(result.Data);
            return new SuccessResponseViewModelT<GetFacilityResponseViewModel>(data);
        }

        [HttpPost]
        public async Task<ResponseViewModel> AddFacilityAsync(AddFacilityViewModel addFacility)
        { 
            var validator = new AddFacilityViewModelValidator().Validate(addFacility);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidFacilityData, "Invalid Facility Data From Request !!");
            var facilityDto = _mapper.Map<AddFacilityDto>(addFacility);
            var result = await _facilityService.AddFacilityAsync(facilityDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.FacilityAlreadyExists, "Facility Alreagy Exist !!");
            return new SuccessResponseViewModel();
        }
        [HttpPut]
        public async Task<ResponseViewModel> DeleteFacility(Guid id)
        {
            if (id == Guid.Empty) return new FailedResponseViewModel(ErrorType.InvalidFacilityId, "Facility Id Is Required !!");

            var result = await _facilityService.DeleteFacilityAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.InvalidFacilityData, "Facility Already Cancelled!!");

            return new SuccessResponseViewModel("Canceled Successfully");
            
        }
        [HttpGet]
        public async Task<ResponseViewModel> GetRoomFacilities(Guid roomid)
        {
            var result = await _facilityService.GetRoomFacilitiesByRoomIdAsync(roomid);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.FacilityNotFound, "Facility not found !");
            var data = _mapper.Map<List<GetFacilityResponseViewModel>>(result);
            return new SuccessResponseViewModelT<List<GetFacilityResponseViewModel>>(data);
        }
    }
}
