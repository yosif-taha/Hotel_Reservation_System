using Hotel.Shared.Dtos.Rooms;
using Hotel.Shared.ResultPattern;
using Hotel.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Abstractions
{
    public interface IRoomService
    {
        ResultT<PagedResult<GetRoomResponseDto>> GetAllRooms(GetAllRoomsWithPaginationDto dto);
        Task<ResultT<GetRoomResponseDto>> GetRoomByIdAsync(Guid id);
        Task<Result> AddRoomAsync(AddRoomDto createRoomDto);
        Task<Result> UpdateRoomAsync(Guid id, UpdateRoomDto updateRoomDto);
        Task<Result> DeleteRoomAsync(Guid id);
        Task<Result> CheckRoomAvailableAsync(Guid id);
        Task<Result> SetRoomNotAvailableAsync(Guid id);
    }
}
