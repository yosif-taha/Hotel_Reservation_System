using Hotel.Services.Dtos.Offer;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IOfferService
    {
        Task<ResultT<IEnumerable<GetOfferResponseDto>>> GetAllOffers(GetAllOffersWithPaginationDto dto);
        Task<ResultT<GetOfferResponseDto>> GetOfferByIdAsync(Guid id);
        Task<Result> AddOfferAsync(AddOfferDto createOfferDto);
        Task<Result> UpdateOfferAsync(Guid id, UpdateOfferDto updateOfferDto);
        Task<Result> DeleteOfferAsync(Guid id);
        Task<Result> AssignOfferToRoom(Guid offerId, Guid roomId);
        //Task<Result> ApplyBestOfferAsync(Guid roomId, decimal basePrice, DateTime checkIn);

    }
}
