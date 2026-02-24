using FluentValidation;
using Hotel.Presentation.ViewModels.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Facilities
{
    public class GetAllFeedbacksWithPaginationViewModelValidator : AbstractValidator<GetAllFeedbacksWithPaginationViewModel>
    {
        public GetAllFeedbacksWithPaginationViewModelValidator()
        {
            RuleFor(x => x.PageNumber)
               .GreaterThan(0).WithMessage("Page number must be greater than 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
        }
    }
    }

