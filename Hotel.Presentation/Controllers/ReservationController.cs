using AutoMapper;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Presentation.Helpers;
using Hotel.Presentation.Validations.Reservations;
using Hotel.Presentation.ViewModels.Reservation;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Presentation.ViewModels.Rooms;
using Hotel.Services.Dtos.Reservation;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationController(IReservationService _reservationService , IMapper _mapper) : ControllerBase
    {
        [HttpGet]        
        public async Task<ResponseViewModel> GetAllReservations([FromQuery] GetAllReservationsWithPaginationViewModel model)
        {
            var validator = new GetAllRoomsWithPaginationViewModelValidator().Validate(model);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidReservationData, "Invalid Reservation Data From Request");
            var requestDto = _mapper.Map<GetAllReservationsWithPaginationDto>(model);
            var reservations= await _reservationService.GetAllReservations(requestDto);
            var items = _mapper.Map<IEnumerable<GetReservationsResponseViewModel>>(reservations.Data);
            int totalCount = items.Count();
            var result = new PagedResult<GetReservationsResponseViewModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
            };
            return new SuccessResponseViewModelT<PagedResult<GetReservationsResponseViewModel>>(result);
        }
        [HttpGet("Id")]
        public async Task<ResponseViewModel> GetReservation(Guid id)
        {
            var result = await _reservationService.GetReservationAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.ReservationNotFound,"Reservartion not found !");
            var data = _mapper.Map<GetReservationViewModel>(result.Data);

            return new SuccessResponseViewModelT<GetReservationViewModel>(data);
        }
        [HttpPost]
        public async Task<ResponseViewModel> AddReservationAsync(AddReservationViewModel addReservation)
        { 
            var validator = new AddReservationViewModelValidator().Validate(addReservation);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidReservationData, "Invalid Reservation Data From Request !!");
            var reservationDto = _mapper.Map<AddReservationDto>(addReservation);
            var result = await _reservationService.AddReservationAsync(reservationDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.ReservationAlreadyExists, "Reservation Alreagy Exist !!");
            return new SuccessResponseViewModel();
        }
        [HttpPut("Cancel")]
        public async Task<ResponseViewModel> CancelReservation(Guid id)
        {
            if (id == Guid.Empty) return new FailedResponseViewModel(ErrorType.InvalidReservationId, "Reservation Id Is Required !!");

            var result = await _reservationService.CancelReservation(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.InvalidReservationData, "Reservation Already Not Found!!");

            return new SuccessResponseViewModel("Canceled Successfully");
            
        }
    }
}
