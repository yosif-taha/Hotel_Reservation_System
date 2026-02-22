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
            RuleFor(x => x.PageSize)
       .GreaterThan(0)
       .LessThanOrEqualTo(100)
       .WithMessage("Page size must be between 1 and 100");
            RuleFor(x => x.Title)
                .MaximumLength(100)
                .WithMessage("Title cannot exceed 100 characters");

        }
    }

}
