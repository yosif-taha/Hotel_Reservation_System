using AutoMapper;
using Hotel.Presentation.Validations.Offers;
using Hotel.Presentation.ViewModels.Offers;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.ResultPattern;
using Hotel.Services.Services;
using Hotel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Dtos.Offer;
using Hotel.Presentation.Helpers;
using Hotel.Services.Dtos.Reservation;


namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController(IOfferService _offerService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseViewModel> GetAllOffers(GetAllOffersWithPaginationViewModel model)
        {
            var validator = new GetAllOffersWithPaginationViewModelValidator().Validate(model);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Invalid Offer Data From Request");
            var requestDto = _mapper.Map<GetAllOffersWithPaginationDto>(model);
            var offers = await _offerService.GetAllOffers(requestDto);
            var items = _mapper.Map<IEnumerable<GetAllOffersViewModel>>(offers.Data);
            int totalOffers = items.Count();
            var result = new PagedResult<GetAllOffersViewModel>
            {
                Items = items,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = totalOffers
            };
            return new SuccessResponseViewModelT<PagedResult<GetAllOffersViewModel>>(result);

        }
        [HttpGet("id")]
        public async Task<ResponseViewModel> GetOfferById(Guid id)
        {
            var result = await _offerService.GetOfferByIdAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Offer not found !");
            var data = _mapper.Map<GetOfferResponseViewModel>(result.Data);
            return new SuccessResponseViewModelT<GetOfferResponseViewModel>(data);
        }
        [HttpPost]
        public async Task<ResponseViewModel> AddOffer(AddOfferViewModel addOffer)
        {
            var validator = new AddOfferViewModelValidator().Validate(addOffer);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Invalid Offer Data From Request !!");
            var offerDto = _mapper.Map<Services.Dtos.Offer.AddOfferDto>(addOffer);
            var result = await _offerService.AddOfferAsync(offerDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Failed to add offer !!");
            return new SuccessResponseViewModel();
        }
        [HttpPut("Update")]
        public async Task<ResponseViewModel> UpdateOffer(Guid id, UpdateOfferViewModel updateOffer)
        {
            var validator = new UpdateOfferViewModelValidator().Validate(updateOffer);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Invalid Offer Data From Request !!");
            var offerDto = _mapper.Map<UpdateOfferDto>(updateOffer);
            var result = await _offerService.UpdateOfferAsync(id, offerDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.OfferNotFound, "Offer not found !!");
            return new SuccessResponseViewModel();
        }
        [HttpPut("Delete")]
        public async Task<ResponseViewModel> DeleteOffer(Guid id)
        {
            var result = await _offerService.DeleteOfferAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.OfferNotFound, "Offer not found !!");
            return new SuccessResponseViewModel();
        }
        [HttpPost("AssignOfferToRoom")]
        public async Task<ResponseViewModel> AssignOfferToRoom([FromQuery] Guid roomId, [FromQuery] Guid offerId)
        {
            var result = await _offerService.AssignOfferToRoom(offerId, roomId);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(ErrorType.InvalidOfferData, "InvalidData");

            var data = _mapper.Map<GetOfferResponseViewModel>(result.Data);

            return new SuccessResponseViewModelT<GetOfferResponseViewModel>(data);
        }

    }
}
