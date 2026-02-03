using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Reservation;
using Hotel.Services.Helpers;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;

namespace Hotel.Services.Rooms
{
    public class ReservationService(IGenericRepository<Reservation> _repository, IGenericRepository<Room> _roomRepository,IRoomRepository room, IAsyncQueryExecutor _executor, IMapper _mapper) : IReservationService
    {
        public async Task<ResultT<IEnumerable<GetReservationsResponseDto>>> GetAllReservations(GetAllReservationsWithPaginationDto dto)
        { 
            var query =  _repository.GetAll();
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
            #region GRUD Operations
            var validationResult = validateReservationInput(dto);
            if (!validationResult.IsSuccess) return validationResult;

            // Determine dates
            var checkIn = dto.CheckInDate?.Date ?? DateTime.UtcNow.Date;
            var stayDays = dto.StayDays ?? 5;
            var checkOut = checkIn.AddDays(stayDays);

            // Check availability
            foreach (var roomId in dto.RoomIds)
            {
                var isAvailable = await room.CheckAvailabilityAsync(roomId, checkIn, checkOut);
                if (!isAvailable)
                    return Result.Failure(new Error(ErrorCode.NotAvailable, "One or more rooms are not available"));
            }

            // Create Reservation
            var reservation = _mapper.Map<Reservation>(dto);

            // Create ReservationRooms
            decimal totalPrice = 0;

            foreach (var roomId in dto.RoomIds)
            {
                var query = _roomRepository.GetById(roomId);
                var room = query.ProjectTo<Room>(_mapper.ConfigurationProvider).FirstOrDefault();
                if (room == null) return Result.Failure(new Error(ErrorCode.NotFound, "Room not found"));

                totalPrice += room.PricePerNight * stayDays;

                reservation.ReservationRooms.Add(new ReservationRoom
                {
                    RoomId = roomId,
                    PricePerNight = room.PricePerNight,
                    NumberOfNights = stayDays
                });
            }

            reservation.TotalPrice = totalPrice;

            // Save
            await _repository.AddAsync(reservation);
            return Result.Success();
        }
        #endregion

        #region Private Helpers 
        private Result validateReservationInput(AddReservationDto dto)
        {
            if (dto == null)
                return Result.Failure(new Error(ErrorCode.InvalidData, "Input data is required"));
            if (dto.RoomIds == null || !dto.RoomIds.Any())
                return Result.Failure(new Error(ErrorCode.InvalidData, "At least one room is required"));
            if (dto.CheckInDate.HasValue && dto.CheckInDate.Value.Date < DateTime.UtcNow.Date)
                return Result.Failure(new Error(ErrorCode.InvalidData, "Check-in date cannot be in the past"));
            return Result.Success();
        }
        #endregion
    }
}
