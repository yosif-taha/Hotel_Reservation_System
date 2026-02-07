using AutoMapper;
using Hotel.Presentation.Validations.Offers;
using Hotel.Presentation.ViewModels.Offer;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.Dtos.Offer;
using Hotel.Services.ResultPattern;
using Hotel.Services.Services;
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
    public class OfferController(OfferService _offerService,IMapper _mapper) : ControllerBase
    {
        [HttpPost("AddOffer")]
        public async Task<ResponseViewModel> AddOffer(AddOfferViewModel addOfferVM)
        {
            var validator = new AddOfferViewModelValidator().Validate(addOfferVM);
            if (!validator.IsValid)
                return new FailedResponseViewModel(ErrorType.InvalidOfferData, "Invalid Offer Request");

            var offerDto = _mapper.Map<AddOfferDto>(addOfferVM);

            var result = await _offerService.AddOfferAsync(offerDto);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(ErrorType.OfferBusinessError, result.Error.Message);

            return new SuccessResponseViewModel("Offer added successfully");
        }
    }
}
