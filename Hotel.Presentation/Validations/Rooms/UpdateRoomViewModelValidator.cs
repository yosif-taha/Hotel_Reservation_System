using FluentValidation;
using Hotel.Shared.ViewModels.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Rooms
{
    public class UpdateRoomViewModelValidator : AbstractValidator<UpdateRoomViewModel>
    {
        public UpdateRoomViewModelValidator()
        {
            RuleFor(x => x.RoomNumber)
                .GreaterThan(0).When(x => x.RoomNumber.HasValue)
                .WithMessage("Room number must be greater than 0.");
            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).When(x => x.PricePerNight.HasValue)
                .WithMessage("Price per night must be greater than 0.");
            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description must be at least 10 characters long.");

        }
    }
}
