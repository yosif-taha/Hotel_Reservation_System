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
    public class OfferService(IGenericRepository<Offer> _offerRepository,IRoomRepository _roomRepository,IAsyncQueryExecutor _executor,IMapper _mapper) : IOfferService
    {
        public async Task<ResultT<IEnumerable<GetOfferResponseDto>>> GetAllOffers(GetAllOffersWithPaginationDto dto)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}
