using FluentValidation;
using Hotel.Presentation.ViewModels.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Rooms
{
    public class AddRoomViewModelValidator : AbstractValidator<AddRoomViewModel>
    {
        public AddRoomViewModelValidator()
        {
            RuleFor(x => x.RoomNumber)
                .GreaterThan(0)
                .WithMessage("Room number must be greater than 0.")
                .NotEmpty()
                .WithMessage("Room number is required.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0)
                .WithMessage("Price per night must be greater than 0.")
                .NotEmpty()
                .WithMessage("Price per night is required.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description must be at least 10 characters long.");

            //RuleFor(x => x.IsAvailable)
            //    .NotNull()
            //    .WithMessage("Availability status is required.");

            RuleFor(x => x.RoomTypeId)
                .NotEmpty()
                .WithMessage("Room type ID is required.");
        }
    }
}
