using AutoMapper;
using Hotel.Presentation.Validations.Rooms;
using Hotel.Services.Abstractions;
using Hotel.Shared.Dtos.Rooms;
using Hotel.Shared.Helpers;
using Hotel.Shared.ViewModels.Response;
using Hotel.Shared.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IRoomService _roomService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("GetAll")]
        public ResponseViewModel GetRooms([FromQuery] GetAllRoomsWithPaginationViewModel model)
        {
            var validator = new GetAllRoomsWithPaginationViewModelValidator().Validate(model);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidRoomData,"Invalid Room Data From Request");
            var requestDto = _mapper.Map<GetAllRoomsWithPaginationDto>(model);
            var rooms = _roomService.GetAllRooms(requestDto);
            var items = _mapper.Map<IEnumerable<GetAllRoomsResponseViewModel>>(rooms.Data.Items);
            var result = new PagedResult<GetAllRoomsResponseViewModel>
            {
                Items = items.ToList(),
                TotalCount = rooms.Data.TotalCount,
                PageNumber = rooms.Data.PageNumber,
                PageSize = rooms.Data.PageSize
            };
            return new SuccessResponseViewModel<PagedResult<GetAllRoomsResponseViewModel>>(result);
        }

        [HttpGet("GetBy{id}")]
        public async Task<ResponseViewModel> GetRoom(Guid id)
        {
            var result = await _roomService.GetRoomByIdAsync(id);
            if(!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, "Room Data Not Found !!");
            var data = _mapper.Map<GetRoomViewModel>(result.Data); 
            return new SuccessResponseViewModel<GetRoomViewModel>(data);   
        }

        [HttpPost("Add")]
        public async Task<ResponseViewModel> AddRoom([FromBody] AddRoomViewModel addRoom)
        {     
            var validator = new AddRoomViewModelValidator().Validate(addRoom);
            if (!validator.IsValid)   return new FailedResponseViewModel(ErrorType.InvalidRoomData,"Invalid Room Data From Request !!");
            var roomDto = _mapper.Map<AddRoomDto>(addRoom);
            var result = await _roomService.AddRoomAsync(roomDto);
            if (!result.IsSuccess)      return new FailedResponseViewModel(ErrorType.RoomAlreadyExists,"Room Alreagy Exist !!");
            var dataModel = _mapper.Map<GetRoomViewModel>(result);
            return new SuccessResponseViewModel<GetRoomViewModel>(dataModel);
        }

        [HttpPut("Update")]
        public async Task<ResponseViewModel> UpdateRoom(Guid id, [FromBody] UpdateRoomViewModel updateRoom)
        {
            var validator = new UpdateRoomViewModelValidator().Validate(updateRoom);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidRoomData,"Invalid Room DataFrom Request");
            var roomDto = _mapper.Map<UpdateRoomDto>(updateRoom);
            var result = await _roomService.UpdateRoomAsync(id, roomDto);
            if (!result.IsSuccess )         return new FailedResponseViewModel(ErrorType.RoomAlreadyExists, "Room With Data is Already Exists Or Room Not Found");
            return new SuccessResponseViewModel<string>("Updated Successfully");
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ResponseViewModel> DeleteCourse(Guid id)
        {
            if (id == null) return new FailedResponseViewModel(ErrorType.InvalidRoomId,"Room Id Is Required !!");
             var result = await _roomService.DeleteRoomAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, "Room Already Not Found!!");
            return new SuccessResponseViewModel<string>("Deleted Room Successfully");
        }

        [HttpGet("CkeckAvailabilityBy{id}")]
        public async Task<ResponseViewModel> CheckRoomAvailability(Guid id)
        {
            if (id == null) return new FailedResponseViewModel(ErrorType.InvalidRoomId, "Room Id Is Required !!");
            var result = await _roomService.CheckRoomAvailableAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, $"Room Is Not Found Or Not Available");
            return new SuccessResponseViewModel<string>("Room Is Available");
        }

        [HttpPut("SetNotAvailable")]
        public async Task<ResponseViewModel> SetRoomNotAvailable([FromQuery]Guid id)
        {
            if (id == null) return new FailedResponseViewModel(ErrorType.InvalidRoomId, "Room Id Is Required !!");
            var result =  await _roomService.SetRoomNotAvailableAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, $"Room Is Not Found Or Not Available");
            return new SuccessResponseViewModel<string>($"Room marked as unavailable");
        }


    }
}
