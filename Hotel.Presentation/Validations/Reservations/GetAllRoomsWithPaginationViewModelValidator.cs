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
                .LessThanOrEqualTo(x => x.CheckOutDate)
                .WithMessage("Check-in date must be earlier than or equal to check-out date.");
            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total price must be greater than or equal to 0.");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Room number must be greater than 0.");
            RuleFor(x => x.PageSize)
                .ExclusiveBetween(0, 101)
                .WithMessage("Price per night must be greater than 0.");
        }

    }
}
