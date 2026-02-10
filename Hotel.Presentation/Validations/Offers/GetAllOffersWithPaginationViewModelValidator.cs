using FluentValidation;
using Hotel.Presentation.ViewModels.Offer;
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
            // Pagination validation
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Page size cannot exceed 100");

            // Date range validation (only if both provided)
            When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
            {
                RuleFor(x => x.EndDate)
                    .GreaterThan(x => x.StartDate)
                    .WithMessage("End date must be after start date");
            });

            // Discount validation (only if provided)
            When(x => x.DiscountPercentage.HasValue, () =>
            {
                RuleFor(x => x.DiscountPercentage!.Value)
                    .GreaterThan(0)
                    .WithMessage("Discount must be greater than 0")
                    .LessThanOrEqualTo(100)
                    .WithMessage("Discount cannot exceed 100%");
            });
        }
    }

}
