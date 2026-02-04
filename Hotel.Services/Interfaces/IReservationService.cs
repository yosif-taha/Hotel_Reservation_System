using Hotel.Services.Dtos.Reservation;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Contracts
{
    public interface IReservationService
    {
        Task<ResultT<IEnumerable<GetReservationsResponseDto>>> GetAllReservations(GetAllReservationsWithPaginationDto dto);
        Task<Result> AddReservationAsync(AddReservationDto createReservationDto);
        Task<ResultT<GetByIdReservationDTO>> GetReservationAsync(Guid Id);
        Task<Result> CancelReservation(Guid Id);

    }
}
