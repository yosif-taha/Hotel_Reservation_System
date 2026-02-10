using FluentValidation;
using Hotel.Presentation.ViewModels.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Offers
{
    public class GetAllOffersWithPaginationViewModelValidator
      : AbstractValidator<GetAllOffersWithPaginationViewModel>
    {
        public GetAllOffersWithPaginationViewModelValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0");
        }
    }

}
