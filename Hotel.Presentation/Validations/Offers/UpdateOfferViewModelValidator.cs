using FluentValidation;
using Hotel.Presentation.ViewModels.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Offers
{
    public class UpdateOfferViewModelValidator:AbstractValidator<UpdateOfferViewModel>
    {       
        public UpdateOfferViewModelValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(100)
                .WithMessage("Title must be at most 100 characters long.");
            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("Start date must be before end date.");
            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Discount percentage must be between 0 and 100.");
        }
    }
}
