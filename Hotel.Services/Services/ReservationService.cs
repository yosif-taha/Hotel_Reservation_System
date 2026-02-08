using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Domin.Entities.Enums;
using Hotel.Services.Dtos.Reservation;
using Hotel.Services.Helpers;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;

namespace Hotel.Services.Rooms
{
    public class ReservationService(IGenericRepository<Reservation> _reservationRepository,IRoomRepository _roomRepository, IAsyncQueryExecutor _executor, IMapper _mapper) : IReservationService
    {
        #region GRUD Operations
        // Validate userId ==>
        public async Task<ResultT<IEnumerable<GetReservationsResponseDto>>> GetAllReservations(GetAllReservationsWithPaginationDto dto)
        { 
            var query = _reservationRepository.GetAll();
            var expression = ExpressionBuilder.BuildFilterExpression<Reservation, GetAllReservationsWithPaginationDto>(dto);
            if (expression != null) query = query.Where(expression);     
            var projectedQuery = query.ProjectTo<GetReservationsResponseDto>(_mapper.ConfigurationProvider);

            var items = projectedQuery
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize);
            var data = await _executor.ToListAsync(items);
            return ResultT<IEnumerable<GetReservationsResponseDto>>.Success(data);
        }


        public async Task<Result> AddReservationAsync(AddReservationDto dto)
        {
            var validationResult = validateReservationInput(dto);
            if (!validationResult.IsSuccess) return validationResult;

            // Determine dates
            var today = DateOnly.FromDateTime(DateTime.Now);
            var checkIn = dto.CheckInDate ?? today;
            var stayDays = dto.StayDays ?? 3;
            var checkOut = checkIn.AddDays(stayDays);

            // Check availability
            var isAvailable = await _roomRepository.AreRoomsAvailableAsync(dto.RoomIds, checkIn, checkOut);
            if (!isAvailable)
                return Result.Failure(new Error(ErrorCode.NotAvailable, "One or more rooms are not available"));

            // Create Reservation
            var reservation = _mapper.Map<Reservation>(dto);

            reservation.TotalPrice = await _roomRepository.CalculateTotalPriceAsync(dto.RoomIds,stayDays);
            reservation.CheckInDate = checkIn;
            reservation.CheckOutDate = checkOut;
            reservation.Status = ReservationStatus.Pending;  // Payment logic Should Make Status Confirmed

            // Save
            await _reservationRepository.AddAsync(reservation);
            return Result.Success();
        }

        public async Task<ResultT<GetByIdReservationDTO>> GetReservationAsync(Guid Id)
        {
            var query = _reservationRepository.GetById(Id);
            var item = query.ProjectTo<GetByIdReservationDTO>(_mapper.ConfigurationProvider);
            var result = await _executor.FirstOrDefaultAsync(item);
            if (result == null) return ResultT<GetByIdReservationDTO>.Failure(new Error(ErrorCode.NotFound, "Reservation is not found !"));
            return ResultT<GetByIdReservationDTO>.Success(result);
        }

        public async Task<Result> CancelReservation(Guid Id)
        {
            // 1. Get reservation entity directly
            var query = _reservationRepository.GetById(Id).Cast<Reservation>();
            var reservation = await _executor.FirstOrDefaultAsync<Reservation>(query);

            if (reservation == null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Reservation not found!"));

            // 2. Check if already cancelled
            if (reservation.Status == ReservationStatus.Cancelled)
                return Result.Failure(new Error(ErrorCode.AlreadyExists, "Reservation already cancelled."));

            // 3. Cancel reservation
            reservation.Status = ReservationStatus.Cancelled;

            // 4. Update in database
            _reservationRepository.Update(reservation, nameof(Reservation.Status));

            return Result.Success();
        }
        #endregion
        #region Business Logic
        // Add any additional business logic methods here if needed in the future
        public async Task<Result> UpdateRoomReservation(Guid Id) { 

            throw new NotImplementedException();
        }

        #endregion
        #region Private Helpers 
        private Result validateReservationInput(AddReservationDto dto)
        {
            if (dto == null)
                return Result.Failure(new Error(ErrorCode.InvalidData, "Input data is required"));
            if (dto.RoomIds == null || !dto.RoomIds.Any())
                return Result.Failure(new Error(ErrorCode.InvalidData, "At least one room is required"));
            if (dto.CheckInDate.HasValue && dto.CheckInDate.Value < DateOnly.FromDateTime(DateTime.UtcNow))
                return Result.Failure(new Error(ErrorCode.InvalidData, "Check-in date cannot be in the past"));
            return Result.Success();
        }
        #endregion

    }
}
