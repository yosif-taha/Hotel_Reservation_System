using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Facility;
using Hotel.Services.Dtos.Offer;
using Hotel.Services.Helpers;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;

namespace Hotel.Services.Services
{
    public class FacilityService(IGenericRepository<Facility> _facilityRepository,IRoomRepository _roomRepository, IAsyncQueryExecutor _executor, IMapper _mapper) : IFacilityService
    {
        #region Add CRUD operations
        public async Task<ResultT<IEnumerable<GetFacilityResponseDto>>> GetAllFacilities(GetAllFacilitiesWithPaginationDto dto)
        {
            var query = _facilityRepository.GetAll();
            var expression = ExpressionBuilder.BuildFilterExpression<Facility, GetAllFacilitiesWithPaginationDto>(dto);
            if (expression != null) query = query.Where(expression);
            var data = query.ProjectTo<GetFacilityResponseDto>(_mapper.ConfigurationProvider);
            var items = data.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);
            var result = await _executor.ToListAsync(items);
            if (data == null) return ResultT<IEnumerable<GetFacilityResponseDto>>.Failure(new Error(ErrorCode.NotFound, "No facilities found"));
            return ResultT<IEnumerable<GetFacilityResponseDto>>.Success(result);
        }

        public async Task<ResultT<GetFacilityResponseDto>> GetFacilityByIdAsync(Guid id)
        {
            var query = _facilityRepository.GetById(id).Cast<Facility>();
            var projectedQuery = query.ProjectTo<GetFacilityResponseDto>(_mapper.ConfigurationProvider);
            var facility = await _executor.FirstOrDefaultAsync(projectedQuery);
            if (facility == null) return ResultT<GetFacilityResponseDto>.Failure(new Error(ErrorCode.NotFound, "facility not found"));
            return ResultT<GetFacilityResponseDto>.Success(facility);
        }

        public async Task<Result> AddFacilityAsync(AddFacilityDto dto)
        {
            // Validate input
            var validationResult = validateFacilityInput(dto);
            if (!validationResult.IsSuccess) return validationResult;

            // Check if rooms exist
            var isAvailable = await _roomRepository.CheckRoomsExistAsync(dto.RoomIds);
            if (!isAvailable) return Result.Failure(new Error(ErrorCode.InvalidData, "One or more rooms do not exist"));

            // Save
            await _facilityRepository.AddAsync(_mapper.Map<Facility>(dto));
            return Result.Success();
        }

        public async Task<Result> UpdateFacilityAsync(Guid id, UpdateFacilityDto dto)
        {
            var query = _facilityRepository.GetById(id).Cast<Facility>();
            var facility = await _executor.FirstOrDefaultAsync(query);

            if (facility == null)
                return Result.Failure(new Error(ErrorCode.NotFound, "facility not found"));

            var modifiedProps = new List<string>();

            foreach (var prop in typeof(UpdateFacilityDto).GetProperties())
            {
                var value = prop.GetValue(dto);
                if (value == null) continue;

                var entityProp = typeof(Offer).GetProperty(prop.Name);
                if (entityProp == null) continue;

                entityProp.SetValue(facility, value);
                modifiedProps.Add(entityProp.Name);
            }

            _facilityRepository.Update(facility, modifiedProps.ToArray());
            return Result.Success();
        }

        public async Task<Result> DeleteFacilityAsync(Guid id)
        {
            var query = _facilityRepository.GetById(id).Cast<Facility>();
            var facility = await _executor.FirstOrDefaultAsync(query);
            
            if (facility == null)
                return Result.Failure(new Error(ErrorCode.NotFound, "facility not found"));

             _facilityRepository.SoftDelete(facility.Id);
            return Result.Success();
        }
        #endregion

        #region Bisiness logic operations
        public async Task<ResultT<IEnumerable<GetFacilityResponseDto>>> GetRoomFacilitiesByRoomIdAsync(Guid roomId)
        {
            var isExist = await _roomRepository.CheckRoomExistAsync(roomId);
            if (!isExist)
                return ResultT<IEnumerable<GetFacilityResponseDto>>.Failure(new Error(ErrorCode.NotFound, "Room not found"));

            var query = _facilityRepository.GetAll().Where(f => f.RoomFacilities.Any(rf => rf.RoomId == roomId));
            var projectedQuery=query.ProjectTo<GetFacilityResponseDto>(_mapper.ConfigurationProvider);
            var facilities = await _executor.ToListAsync(projectedQuery);
            if (facilities == null || !facilities.Any())
                return ResultT<IEnumerable<GetFacilityResponseDto>>.Failure(new Error(ErrorCode.NotFound, "No facilities found for the specified room"));
            return ResultT<IEnumerable<GetFacilityResponseDto>>.Success(facilities);
        }
        #endregion

        #region Private helper methods
        private Result validateFacilityInput(AddFacilityDto dto) 
        {
           if (dto == null)
                return Result.Failure(new Error(ErrorCode.InvalidData, "Input data is required"));
            if (dto.RoomIds == null || !dto.RoomIds.Any())
                return Result.Failure(new Error(ErrorCode.InvalidData, "At least one room is required"));
            if (dto.Name==null)
                return Result.Failure(new Error(ErrorCode.InvalidData, "Name Facility is required"));
            return Result.Success();
        }
        #endregion
    }
}
