using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Offer;
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
    public class OfferService(IGenericRepository<Offer> _offerRepository,IGenericRepository<Room> _roomRepository,IAsyncQueryExecutor _executor,IMapper _mapper) : IOfferService
    {
        public async Task<ResultT<IEnumerable<GetOfferResponseDto>>> GetAllOffers(GetAllOffersWithPaginationDto dto)
        {
            var query = _offerRepository.GetAll();

            var expression = ExpressionBuilder.BuildFilterExpression<Offer, GetAllOffersWithPaginationDto>(dto);
            if (expression is not null)
                query = query.Where(expression);

            var data = query.ProjectTo<GetOfferResponseDto>(_mapper.ConfigurationProvider);

            var items = data.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);

            var result = await _executor.ToListAsync(items);
            return ResultT<IEnumerable<GetOfferResponseDto>>.Success(result);

        }

        public async Task<ResultT<GetOfferResponseDto>> GetOfferByIdAsync(Guid id)
        {
            var query = _offerRepository.GetById(id)
                .ProjectTo<GetOfferResponseDto>(_mapper.ConfigurationProvider);

            var result = await _executor.FirstOrDefaultAsync(query);
            if (result == null)
                return ResultT<GetOfferResponseDto>.Failure(new Error(ErrorCode.NotFound, "Offer not found"));

            return ResultT<GetOfferResponseDto>.Success(result);
        }

        public async Task<Result> AddOfferAsync(AddOfferDto dto)
        {
            var IsExist = await GetOfferByIdAsync(dto.Id);
            if (IsExist.IsSuccess) return Result.Failure(new Error(ErrorCode.AlreadyExists, "Offer already exist !!"));
            var Data = _mapper.Map<Offer>(dto);
            var result =  _offerRepository.AddAsync(Data);
            return Result.Success();
        }

        public async Task<Result> UpdateOfferAsync(Guid id, UpdateOfferDto dto)
        {
            var query = _offerRepository.GetById(id).Cast<Offer>();
            var offer = await _executor.FirstOrDefaultAsync(query);

            if (offer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Offer not found"));

            var modifiedProps = new List<string>();

            foreach (var prop in typeof(UpdateOfferDto).GetProperties())
            {
                var value = prop.GetValue(dto);
                if (value == null) continue;

                var entityProp = typeof(Offer).GetProperty(prop.Name);
                if (entityProp == null) continue;

                entityProp.SetValue(offer, value);
                modifiedProps.Add(entityProp.Name);
            }

            _offerRepository.Update(offer, modifiedProps.ToArray());
            return Result.Success();
        }


        public async Task<Result> DeleteOfferAsync(Guid id)
        {
            var existing = await GetOfferByIdAsync(id);

            if (existing == null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Offer not found"));

            _offerRepository.SoftDelete(id);
            return Result.Success();
        }

        public async Task<ResultT<GetOfferResponseDto>> AssignOfferToRoom(Guid offerId, Guid roomId)
        {
         
            var roomQuery = _roomRepository.GetById(roomId);
            var roomEntity = await _executor.FirstOrDefaultAsync(roomQuery);
            if (roomEntity == null)
                return ResultT<GetOfferResponseDto>.Failure(new Error(ErrorCode.NotFound, "Room not found !!"));

        
            var offerResult = await GetOfferByIdAsync(offerId);
            if (!offerResult.IsSuccess)
                return ResultT<GetOfferResponseDto>.Failure(new Error(ErrorCode.NotFound, "Offer not found !!"));
            var offer = offerResult.Data;


            if(!roomEntity.OfferRooms.Any(o=>o.OfferId == offerId))
            {
                roomEntity.OfferRooms.Add(new OfferRoom { OfferId = offerId, RoomId = roomId });
            }
            _roomRepository.Update(roomEntity);

            return ResultT<GetOfferResponseDto>.Success(offer);
        }

    }
}
