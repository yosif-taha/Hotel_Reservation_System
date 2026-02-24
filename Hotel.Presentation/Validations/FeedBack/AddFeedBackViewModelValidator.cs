using FluentValidation;
using Hotel.Presentation.ViewModels.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.FeedBack
{
    public class AddFeedBackViewModelValidator : AbstractValidator<AddFeedBackViewModel>
    {
        public AddFeedBackViewModelValidator()
        {
            RuleFor(fb => fb.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5");

            RuleFor(fb => fb.Comment)
                .NotEmpty()
                .WithMessage("Comment is required")
                .MaximumLength(1000)
                .WithMessage("Comment must not exceed 1000 characters");
            RuleFor(fb => fb.Comment)
                .Must(c => !string.IsNullOrWhiteSpace(c))
                .WithMessage("Comment cannot be empty or whitespace");

            RuleFor(fb => fb.RoomId)
                .NotEmpty()
                .WithMessage("RoomId is required");

            
            RuleFor(fb => fb.ReservationId)
                .NotEmpty()
                .WithMessage("ReservationId is required");

        }
    }
}
