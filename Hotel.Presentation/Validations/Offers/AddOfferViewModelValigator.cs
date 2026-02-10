using FluentValidation;
using Hotel.Presentation.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Offers
{
    public class AddOfferViewModelValidator : AbstractValidator<AddOfferViewModel>
    {
        public AddOfferViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");

            RuleFor(x => x.DiscountPercentage)
                .NotEmpty()
                .WithMessage("Put the Discount Percentage")
                .GreaterThan(0)
                .WithMessage("Discount must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Discount cannot exceed 100%");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the past");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required")
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after start date");
        }
    }

}
