using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Presentation.Helpers;
using Hotel.Presentation.Validations.Facilities;
using Hotel.Presentation.Validations.FeedBack;
using Hotel.Presentation.ViewModels.FeedBack;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.Dtos.FeedBack;
using Hotel.Services.Interfaces;
using Hotel.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FeedbackController(IFeedBackService _feedBackService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseViewModel> GetAllFeedbacks([FromQuery] GetAllFeedbacksWithPaginationViewModel viewModel)
        {
            var validator = new GetAllFeedbacksWithPaginationViewModelValidator().Validate(viewModel);
            if (!validator.IsValid)
                return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, "Invalid feedback data");
            var data = _mapper.Map<GetAllFeedbackWithPaginationDto>(viewModel);
            var result = await _feedBackService.GetAllFeedbackAsync(data);
            if (!result.IsSuccess)
                return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, "Invalid feedback data");
            var items = _mapper.Map<IEnumerable<GetFeedbackResponseDto>>(result.Data);
            var totalCount = items.Count();
            var pagedResult = new PagedResult<GetFeedbackResponseDto>()
            {
                Items = items,
                PageNumber = viewModel.PageNumber,
                PageSize = viewModel.PageSize,
                TotalCount = totalCount,
            };
            return new SuccessResponseViewModelT<PagedResult<GetFeedbackResponseDto>>(pagedResult, "Feedbacks retrieved successfully.");
        }
        [HttpGet]
        public async Task<ResponseViewModel> GetFeedbackById([FromQuery] Guid feedbackId)
        {
            var feedback = await _feedBackService.GetFeedbackByIdAsync(feedbackId);
            if (!feedback.IsSuccess)
                return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, "Invalid feedback data");
            var result = _mapper.Map<GetFeedbackResponseViewModel>(feedback.Data);
            return new SuccessResponseViewModelT<GetFeedbackResponseViewModel>(result, "Feedback retrieved successfully.");
        }
        [HttpPost]
        public async Task<ResponseViewModel> AddFeedback([FromBody] AddFeedBackViewModel viewModel)
        {
            var validator = new AddFeedBackViewModelValidator().Validate(viewModel);
            if (!validator.IsValid)
                return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, "Invalid feedback data");
            var data = _mapper.Map<AddFeedBackDto>(viewModel);
            // Get the user ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException();
            //هاتلي الـ UserId اللي متخزن جوه الـ JWT للـ user الحالي وحوله لـ Guid.
            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _feedBackService.AddFeedBackAsync(data, /*Guid.Parse("F2BACEF1-E86E-4B9D-A397-9A494CECDCEC")*/ userId);
            if (!result.IsSuccess)
                return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, result.Message);
            return new SuccessResponseViewModel();
        }
        [HttpPatch]
        public async Task<ResponseViewModel> DeleteFeedback([FromQuery] Guid feedbackId)
        {
            var getIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (getIdClaim == null)
                return new FailedResponseViewModel(ErrorType.UnauthorizedAccess, "User not authorized to delete feedback");
            var userId = Guid.Parse(getIdClaim.Value);
            var result = await _feedBackService.DeleteFeedBackAsync(feedbackId, userId);
            if (!result.IsSuccess)
                return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, result.Message);
            return new SuccessResponseViewModel();
        }
        [HttpPost]
        public async Task<ResponseViewModel> AddStaffResponse([FromQuery] Guid feedbackId, [FromBody] AddStaffResponseViewModel viewModel)
        {
            {
                var validator = new AddStaffResponseViewModelValidator().Validate(viewModel);
                if (!validator.IsValid)
                    return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, "Invalid staff response data");
                var data = _mapper.Map<AddStaffResponseDto>(viewModel);
             //   Get the staff ID from the claims

               var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (staffIdClaim == null)
                    throw new UnauthorizedAccessException();
               // Convert the staff ID from the claim to a Guid
                var staffId = Guid.Parse(staffIdClaim.Value);
                var result = await _feedBackService.AddStaffResponse(feedbackId, data, staffId /*Guid.Parse("55470d87-7908-486c-aad6-0bac12a7325a")*/);
                if (!result.IsSuccess)
                    return new FailedResponseViewModel(ErrorType.InvalidFeedbackData, result.Message);
                return new SuccessResponseViewModel();

            }
        }
    }
}
