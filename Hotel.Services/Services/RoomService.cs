using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Hotel.Services.Interfaces;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.ResultPattern;
using Hotel.Services.Helpers;

namespace Hotel.Services.Services
{
    public class RoomService(IGenericRepository<Room> _repository, IRoomRepository _roomRepository,IAsyncQueryExecutor _executor, IMapper _mapper) : IRoomService
    {
        public  async Task<ResultT<IEnumerable<GetRoomResponseDto>>> GetAllRooms(GetAllRoomsWithPaginationDto dto)
        {
            var query = _repository.GetAll();
            var expression = ExpressionBuilder.BuildFilterExpression<Room, GetAllRoomsWithPaginationDto>(dto);
            if (expression != null) query = query.Where(expression);
            var projectedQuery = query.ProjectTo<GetRoomResponseDto>(_mapper.ConfigurationProvider);

            var items =  projectedQuery
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize);
            var data = await _executor.ToListAsync(items); 
            return ResultT<IEnumerable<GetRoomResponseDto>>.Success(data);
        }
        public async Task<ResultT<GetRoomResponseDto>> GetRoomByIdAsync(Guid id)
        {
            var data = _repository.GetById(id);
            var items = data.ProjectTo<GetRoomResponseDto>(_mapper.ConfigurationProvider);
            var result = await _executor.FirstOrDefaultAsync(items);
            if (result == null) return ResultT<GetRoomResponseDto>.Failure(new Error(ErrorCode.NotFound, "Room IS Not Found !!")); 
            return ResultT<GetRoomResponseDto>.Success(result);
        }
        public async Task<Result> AddRoomAsync(AddRoomDto dto)
        {
            var roomExist = await _roomRepository.ExistsByNumberAsync(dto.RoomNumber);
            if (roomExist) return Result.Failure(new Error(ErrorCode.AlreadyExists, "Room already exists"));
            var room = _mapper.Map<Room>(dto);
            await _repository.AddAsync(room);
            return Result.Success();
        }

        public async Task<Result> UpdateRoomAsync(Guid id, UpdateRoomDto updateRoomDto)
        {
            var data = await GetRoomByIdAsync(id);
            if (!data.IsSuccess) return ResultT<GetRoomResponseDto>.Failure(new Error(ErrorCode.NotFound, "Room IS Not Found !!"));

            if (updateRoomDto.RoomNumber.HasValue)
            {
                var roomExist = await _roomRepository.ExistsByNumberAsync(updateRoomDto.RoomNumber.Value);
                if (roomExist) return Result.Failure(new Error(ErrorCode.AlreadyExists, "Room already exists"));
            }
            var course = new Room { Id = id };
            var modifiedProps = new List<string>();

            foreach (var prop in typeof(UpdateRoomDto).GetProperties())
            {
                var value = prop.GetValue(updateRoomDto);
                if (value == null)
                    continue;

                var entityProp = typeof(Room).GetProperty(prop.Name);
                if (entityProp == null)
                    continue;

                entityProp.SetValue(course, value);
                modifiedProps.Add(entityProp.Name);
            }
            _repository.Update(course, modifiedProps.ToArray());

            return Result.Success();
        }

        public async Task<Result> DeleteRoomAsync(Guid id)
        {
            var data = await GetRoomByIdAsync(id);
            if (!data.IsSuccess) return ResultT<GetRoomResponseDto>.Failure(new Error(ErrorCode.NotFound, "Room IS Not Found !!"));

            _repository.SoftDelete(id);
            return Result.Success();
        }
            
        public async Task<Result> CheckRoomAvailableAsync(Guid id, DateOnly? CheckInDate, DateOnly? CheckoutDate,int? stayDays)
        {
            var data = await GetRoomByIdAsync(id);
            if (!data.IsSuccess) return ResultT<GetRoomResponseDto>.Failure(new Error(ErrorCode.NotFound, "Room IS Not Found !!"));

            // If check-in date is not provided, use today's date as the default check-in date.
            // If stay days is not provided, use 3 days as the default stay duration.
            // If check-out date is not provided, calculate it based on the check-in date and stay duration.
            // Determine dates

            var today = DateOnly.FromDateTime(DateTime.Now);
            var checkIn =CheckInDate ?? today;
            var stayDaynum = stayDays ?? 3;
            var checkOut = CheckoutDate ??checkIn.AddDays(stayDaynum);

            var flag = await _roomRepository.CheckAvailabilityAsync(id,checkIn, checkOut);
            if (!flag) return Result.Failure(new Error(ErrorCode.NotAvailable, $"Room Is Not Available"));
            return Result.Success();
        }

        //public async Task<Result> SetRoomNotAvailableAsync(Guid id)
        //{ 
        //   var dto = new UpdateRoomDto() {IsAvailable = false };
        //   return await UpdateRoomAsync(id,dto);
        //}
    }
}
