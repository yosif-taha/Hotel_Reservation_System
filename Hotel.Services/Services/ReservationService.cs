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
    public class ReservationService(IGenericRepository<Reservation> _reservationRepository, IGenericRepository<ReservationRoom> _roomReservationRepository, IGenericRepository<Room> _roomRepository,IRoomRepository room, IAsyncQueryExecutor _executor, IMapper _mapper) : IReservationService
    {
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

                // change status of reservation ==> Confirmed
                
                reservation.ReservationRooms.Add(new ReservationRoom
                {
                    RoomId = roomId,
                    PricePerNight = room.PricePerNight,
                    NumberOfNights = stayDays,
                    Reservation = new Reservation()
                    {
                        Id = reservation.Id,
                        CheckInDate = checkIn,
                        CheckOutDate = checkOut,
                        Status = ReservationStatus.Pending,
                        UserId = dto.UserId
                    }
                });
            }

            reservation.TotalPrice = totalPrice;

            // Save
            await _reservationRepository.AddAsync(reservation);
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
            var getReservation = await GetReservationAsync(Id);
            if (!getReservation.IsSuccess)
                return Result.Failure(new Error(ErrorCode.NotFound, "Reservation not found !"));

            if (getReservation.Data.Status == ReservationStatus.Cancelled)
                return Result.Failure(new Error(ErrorCode.AlreadyExists, "Reservation already cancelled."));
            var map = _mapper.Map<ReservationRoom>(getReservation);

            map.Reservation.Status = ReservationStatus.Cancelled;
            //var reservation = new ReservationRoom
            //{

            //    Reservation = new()
            //    {
            //        Status = ReservationStatus.Cancelled
            //    }
            //    //Id = getReservation.Data.Id,
            //    //Status = ReservationStatus.Cancelled
            //};
            
            //_roomReservationRepository.Update( ReservationRoom , nameof(map.Reservation.Status));
            return Result.Success();

        }
    }
}
