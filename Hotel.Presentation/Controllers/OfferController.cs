using AutoMapper;
using Hotel.Presentation.Validations.Offers;
using Hotel.Presentation.ViewModels.Offers;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.Dtos.Offer;
using Hotel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController(IOfferService _offerService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseViewModel> GetOfferById(Guid id)
        {
            var result = await _offerService.GetOfferByIdAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Offer not found !");
            var data = _mapper.Map<GetOfferResponseViewModel>(result.Data);
            return new SuccessResponseViewModelT<GetOfferResponseViewModel>(data);
        }
        [HttpPut]
        public async Task<ResponseViewModel> UpdateOffer(Guid id, UpdateOfferViewModel updateOffer)
        {
            var validator = new UpdateOfferViewModelValidator().Validate(updateOffer);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Invalid Offer Data From Request !!");
            var offerDto = _mapper.Map<UpdateOfferDto>(updateOffer);
            var result = await _offerService.UpdateOfferAsync(id, offerDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.OfferNotFound, "Offer not found !!");
            return new SuccessResponseViewModel();
        }
        [HttpPut]
        public async Task<ResponseViewModel> DeleteOffer(Guid id)
        {
            var result = await _offerService.DeleteOfferAsync(id);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.OfferNotFound, "Offer not found !!");
            return new SuccessResponseViewModel();
        }
}
