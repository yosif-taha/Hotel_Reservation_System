using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Services.Abstractions;
using Hotel.Shared.ResultPattern;
using Hotel.Shared.Dtos.Rooms;
using Hotel.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Rooms
{
    public class RoomService(IGenericRepository<Room> _repository, IRoomRepository _roomRepository, IMapper _mapper) : IRoomService
    {
        public ResultT<PagedResult<GetRoomResponseDto>> GetAllRooms(GetAllRoomsWithPaginationDto dto)
        {
            var query = _repository.GetAll();
            var expression = ExpressionBuilder.BuildFilterExpression<Room, GetAllRoomsWithPaginationDto>(dto);
            if (expression != null) query = query.Where(expression);
            var projectedQuery = query.ProjectTo<GetRoomResponseDto>(_mapper.ConfigurationProvider);

            var totalCount = projectedQuery.Count();
            var items = projectedQuery
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();

            var data = new PagedResult<GetRoomResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize
            };
            return ResultT<PagedResult<GetRoomResponseDto>>.Success(data);

        }

        public async Task<ResultT<GetRoomResponseDto>> GetRoomByIdAsync(Guid id)
        {
            var data = await _repository.GetValueAsync(id);
            if (data == null) return ResultT<GetRoomResponseDto>.Failure(new Error("NotFound", "Room IS Not Found !!"));
            var result = _mapper.Map<GetRoomResponseDto>(data);
            return ResultT<GetRoomResponseDto>.Success(result);
        }

        public async Task<Result> AddRoomAsync(AddRoomDto dto)
        {
            var roomExist = await _roomRepository.ExistsByNumberAsync(dto.RoomNumber);
            if (roomExist) return Result.Failure(new Error("Duplicate", "Room already exists"));
            var room = _mapper.Map<Room>(dto);
            await _repository.AddAsync(room);
            return Result.Success();
        }

        public async Task<Result> UpdateRoomAsync(Guid id, UpdateRoomDto updateRoomDto)
        {
            var data = await _repository.GetValueAsync(id);
            if (data == null) return ResultT<GetRoomResponseDto>.Failure(new Error("NotFound", "Room IS Not Found !!"));

            if (updateRoomDto.RoomNumber.HasValue)
            {
                var roomExist = await _roomRepository.ExistsByNumberAsync(updateRoomDto.RoomNumber.Value);
                if (roomExist) return Result.Failure(new Error("Duplicate", "Room already exists"));
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
            var data = await _repository.GetValueAsync(id);
            if (data == null) return ResultT<GetRoomResponseDto>.Failure(new Error("NotFound", "Room IS Not Found !!"));

            _repository.SoftDelete(id);
            return Result.Success();
        }

        public async Task<Result> CheckRoomAvailableAsync(Guid id)
        {
            var data = await _repository.GetValueAsync(id);
            if (data == null) return ResultT<GetRoomResponseDto>.Failure(new Error("NotFound", "Room IS Not Found !!"));
            var flag = await _roomRepository.CheckAvailabilityAsync(id);
           if (!flag) return Result.Failure(new Error("NotAvailable", $"Room Is Not Available"));
           return Result.Success();
        }

        public async Task<Result> SetRoomNotAvailableAsync(Guid id)
        { 
           var dto = new UpdateRoomDto() {IsAvailable = false };
           return await UpdateRoomAsync(id,dto);

        }
    }
}
