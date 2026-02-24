using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Domin.Entities.Enums;
using Hotel.Services.Dtos.FeedBack;
using Hotel.Services.Helpers;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Services
{
    public class FeedBackService(IGenericRepository<Feedback> _feedbackRepository, IGenericRepository<Reservation> _reservationRepository, IMapper _mapper, IAsyncQueryExecutor _executor) : IFeedBackService
    {
        public async Task<ResultT<IEnumerable<GetFeedbackResponseDto>>> GetAllFeedbackAsync(GetAllFeedbackWithPaginationDto getAllFeedbackWithPaginationDto)
        {
            var feedbackQuery = _feedbackRepository.GetAll();
            var expression = ExpressionBuilder.BuildFilterExpression<Feedback, GetAllFeedbackWithPaginationDto>(getAllFeedbackWithPaginationDto);
            if (expression is not null)
                feedbackQuery = feedbackQuery.Where(expression);
            //Mapping
            var projectedQuery = feedbackQuery.ProjectTo<GetFeedbackResponseDto>(_mapper.ConfigurationProvider);
            // Pagination
            var items = projectedQuery
            .Skip((getAllFeedbackWithPaginationDto.PageNumber - 1) * (getAllFeedbackWithPaginationDto.PageSize))
            .Take(getAllFeedbackWithPaginationDto.PageSize);
            // Execute the query and get feedbacks
            var feedbacks = await _executor.ToListAsync(items);
            return ResultT<IEnumerable<GetFeedbackResponseDto>>.Success(feedbacks);

        }

        public async Task<ResultT<GetFeedbackResponseDto>> GetFeedbackByIdAsync(Guid feedbackId)
        {
            var feedbackQuery = _feedbackRepository.GetById(feedbackId)
            .ProjectTo<GetFeedbackResponseDto>(_mapper.ConfigurationProvider);
            var feedback = await _executor.FirstOrDefaultAsync(feedbackQuery);
            if (feedback == null)
                return ResultT<GetFeedbackResponseDto>.Failure(new Error(ErrorCode.NotFound, "Feedback not found."));
            return ResultT<GetFeedbackResponseDto>.Success(feedback);
        }

        public async Task<Result> AddFeedBackAsync(AddFeedBackDto dto, Guid userId)
        {

            var reservationQuery = _reservationRepository
                .GetById(dto.ReservationId, r => r.ReservationRooms);
            var reservation = await _executor.FirstOrDefaultAsync(reservationQuery);
            // لازم نتحقق من أن الحجز موجود وأن المستخدم هو صاحب الحجز قبل السماح له بإضافة تقييم.
            if (reservation == null || reservation.UserId != userId)
                return Result.Failure(new Error(ErrorCode.NotFound, "Reservation not found or unauthorized."));
            // لازم نتحقق من أن الحجز موجود وأن المستخدم هو صاحب الحجز قبل السماح له بإضافة تقييم.
            if (reservation.Status != ReservationStatus.Confirmed) // الافضل نضيف Completed عشان نضمن انه المستخدم ميقدرش يقيم الا لما يخلص فتره اقامته
                return Result.Failure(new Error(ErrorCode.NotAvailable, "Cannot feedback before checkout."));

            // check if room that user feedback is the same as reservation room
            if (!reservation.ReservationRooms.Any(rr => rr.RoomId == dto.RoomId))
                return Result.Failure(new Error(ErrorCode.InvalidData, "Feedback must be for the room in the reservation."));

            // لازم نتحقق من أن المستخدم لم يقيم نفس الغرفة في نفس الحجز من قبل
            var existingFeedbackQuery = await _executor.AnyAsync(_feedbackRepository.GetAll().Where(f => f.ReservationId == dto.ReservationId && f.RoomId == dto.RoomId));
            if (existingFeedbackQuery)
                return Result.Failure(
                    new Error(ErrorCode.AlreadyExists, "Feedback already exists for this room."));

            var feedback = _mapper.Map<Feedback>(dto);
            feedback.UserId = userId;

            await _feedbackRepository.AddAsync(feedback);

            return Result.Success();
        }

        public async Task<Result> DeleteFeedBackAsync(Guid feedbackId, Guid userId)
        {
            var feedbackQuery = _feedbackRepository.GetById(feedbackId);
            var feedback = await _executor.FirstOrDefaultAsync(feedbackQuery);
            if (feedback == null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Feedback not found."));
            if (feedback.UserId != userId)
                return Result.Failure(new Error(ErrorCode.NotAvailable, "You are not authorized to delete this feedback."));
            _feedbackRepository.SoftDelete(feedbackId);
            return Result.Success("Feedback deleted successfully.");

        }
    }
}