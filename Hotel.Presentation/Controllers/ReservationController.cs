using AutoMapper;
using Hotel.Domain.Contracts;
using Hotel.Presentation.ViewModels.Reservation;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.Dtos.Reservation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController(IReservationService _reservationService , IMapper _mapper) : ControllerBase
    {
        [HttpGet("Id")]
        public async Task<ResponseViewModel> GetReservation(Guid id)
        {
            var result = await _reservationService.GetReservationAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.ReservationNotFound,"Reservartion not found !");
            var data = _mapper.Map<GetReservationViewModel>(result.Data);

            return new SuccessResponseViewModelT<GetReservationViewModel>(data);
        }
        [HttpDelete("Delete")]
        public async Task<ResponseViewModel> CancelReservation(Guid id)
        {
            if (id == Guid.Empty) return new FailedResponseViewModel(ErrorType.InvalidReservationId, "Reservation Id Is Required !!");

            var result = await _reservationService.CancelReservation(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.InvalidReservationData, "Reservation Already Not Found!!");

            return new SuccessResponseViewModel("Canceled Successfully");
            
        }
    }
}
