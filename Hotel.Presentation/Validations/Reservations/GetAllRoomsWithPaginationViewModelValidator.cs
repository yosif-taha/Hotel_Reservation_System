using FluentValidation;
using Hotel.Presentation.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Reservations
{
    public class GetAllRoomsWithPaginationViewModelValidator: AbstractValidator<GetAllReservationsWithPaginationViewModel>
    {
        public GetAllRoomsWithPaginationViewModelValidator()
        {
            RuleFor(x => x.CheckInDate)
      .NotEmpty().WithMessage("Check-in date is required.");

            RuleFor(x => x.CheckInDate)
                .Must(date => date!.Value >= DateOnly.FromDateTime(DateTime.UtcNow))
                .When(x => x.CheckInDate.HasValue)
                .WithMessage("Check-in date must be today or later.");

            //RuleFor(x => x.TotalPrice)
            //    .GreaterThanOrEqualTo(0)
            //    .WithMessage("Total price must be greater than or equal to 0.");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Room number must be greater than 0.");
            RuleFor(x => x.PageSize)
                .ExclusiveBetween(0, 101)
                .WithMessage("Price per night must be greater than 0.");
        }

    }
}
