using FluentValidation;
using Hotel.Presentation.ViewModels.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Rooms
{
    public class GetAllRoomsWithPaginationViewModelValidator : AbstractValidator<GetAllRoomsWithPaginationViewModel>
    {
        public GetAllRoomsWithPaginationViewModelValidator()
        {
            RuleFor(x => x.RoomNumber)
                .GreaterThan(0).When(x => x.RoomNumber.HasValue)
                .WithMessage("Room number must be greater than 0.");
            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).When(x => x.PricePerNight.HasValue)
                .WithMessage("Price per night must be greater than 0.");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Room number must be greater than 0.");
            RuleFor(x => x.PageSize)
                   .ExclusiveBetween(0, 101)
                .WithMessage("Price per night must be greater than 0.");

        }
    }
}
