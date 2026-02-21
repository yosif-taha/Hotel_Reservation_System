using FluentValidation;
using Hotel.Presentation.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Reservations
{
    public class AddReservationViewModelValidator : AbstractValidator<AddReservationViewModel>
    {
        public AddReservationViewModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");
            RuleFor(x => x.RoomIds)
                .NotEmpty()
                .WithMessage("At least one Room ID is required.");
            RuleFor(x => x.CheckInDate)
                 .NotEmpty().WithMessage("Check-in date is required.")
                 .Must(date => date!.Value >= DateOnly.FromDateTime(DateTime.Now))
                 .WithMessage("Check-in date must be today or later.");
            RuleFor(x => x.StayDays)
                .GreaterThan(0)
                .WithMessage("Stay days must be greater than 0.")
                .NotEmpty()
                .WithMessage("Stay days is required.");
        }

    }
}
