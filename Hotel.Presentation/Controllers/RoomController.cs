using AutoMapper;
using FluentValidation;
using Hotel.Presentation.Helpers;
using Hotel.Presentation.Validations.Rooms;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Presentation.ViewModels.Rooms;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;
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
        public async Task<ResponseViewModel> GetRooms([FromQuery] GetAllRoomsWithPaginationViewModel model)
        {
            var validator = new GetAllRoomsWithPaginationViewModelValidator().Validate(model);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidRoomData,"Invalid Room Data From Request");
            var requestDto = _mapper.Map<GetAllRoomsWithPaginationDto>(model);
            var rooms = await _roomService.GetAllRooms(requestDto);
            var items = _mapper.Map<IEnumerable<GetAllRoomsResponseViewModel>>(rooms.Data);
            int totalCount = items.Count();
            var result = new PagedResult<GetAllRoomsResponseViewModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
            };
            return new SuccessResponseViewModelT<PagedResult<GetAllRoomsResponseViewModel>>(result);
        }

        [HttpGet("GetBy{id}")]
        public async Task<ResponseViewModel> GetRoom(Guid id)
        {
            var result = await _roomService.GetRoomByIdAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, "Room Not Found !!");
            var data = _mapper.Map<GetRoomViewModel>(result.Data); 
            return new SuccessResponseViewModelT<GetRoomViewModel>(data);   
        }

        [HttpPost("Add")]
        public async Task<ResponseViewModel> AddRoom([FromBody] AddRoomViewModel addRoom)
        {     
            var validator = new AddRoomViewModelValidator().Validate(addRoom);
            if (!validator.IsValid)   return new FailedResponseViewModel(ErrorType.InvalidRoomData,"Invalid Room Data From Request !!");
            var roomDto = _mapper.Map<AddRoomDto>(addRoom);
            var result = await _roomService.AddRoomAsync(roomDto);
            if (!result.IsSuccess)      return new FailedResponseViewModel(ErrorType.RoomAlreadyExists,"Room Alreagy Exist !!");
            return new SuccessResponseViewModel();
        }

        [HttpPut("Update")]
        public async Task<ResponseViewModel> UpdateRoom(Guid id, [FromBody] UpdateRoomViewModel updateRoom)
        {
            var validator = new UpdateRoomViewModelValidator().Validate(updateRoom);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidRoomData,"Invalid Room DataFrom Request");
            var roomDto = _mapper.Map<UpdateRoomDto>(updateRoom);
            var result = await _roomService.UpdateRoomAsync(id, roomDto);
            if (!result.IsSuccess && result.Error.Code == ErrorCode.AlreadyExists)         return new FailedResponseViewModel(ErrorType.RoomAlreadyExists, "New Room Number is Already Exists");
            if (!result.IsSuccess && result.Error.Code == ErrorCode.NotFound)         return new FailedResponseViewModel(ErrorType.RoomNotFound, "Room Not Found");

            return new SuccessResponseViewModel("Updated Successfully");
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ResponseViewModel> DeleteCourse(Guid id)
        {
            if (id == null) return new FailedResponseViewModel(ErrorType.InvalidRoomId,"Room Id Is Required !!");
             var result = await _roomService.DeleteRoomAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, "Room Already Not Found!!");
            return new SuccessResponseViewModel("Deleted Successfully");
        }

        [HttpGet("CkeckAvailabilityBy{id}")]
        public async Task<ResponseViewModel> CheckRoomAvailability(Guid id, DateTime checkIn, DateTime checkOut)
        {
            if (id == null) return new FailedResponseViewModel(ErrorType.InvalidRoomId, "Room Id Is Required !!");
            var result = await _roomService.CheckRoomAvailableAsync(id,checkIn,checkOut);
           if (!result.IsSuccess && result.Error.Code==ErrorCode.NotFound) return new FailedResponseViewModel(ErrorType.RoomNotFound, $"Room Is Not Found");
           if (!result.IsSuccess && result.Error.Code == ErrorCode.NotAvailable) return new SuccessResponseViewModel("Room Is Not Available");
            return new SuccessResponseViewModel("Room Is Available");
        }

        [HttpPut("SetNotAvailable")]
        public async Task<ResponseViewModel> SetRoomNotAvailable([FromQuery]Guid id)
        {
            if (id == null) return new FailedResponseViewModel(ErrorType.InvalidRoomId, "Room Id Is Required !!");
            var result =  await _roomService.SetRoomNotAvailableAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RoomNotFound, $"Room Is Not Found");
            return new SuccessResponseViewModel($"Room marked as unavailable");
        }


    }
}
