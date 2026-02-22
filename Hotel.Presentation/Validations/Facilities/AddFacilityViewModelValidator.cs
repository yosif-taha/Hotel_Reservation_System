using FluentValidation;
using Hotel.Presentation.ViewModels.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Facilities
{
    public class AddFacilityViewModelValidator:AbstractValidator<AddFacilityViewModel>
    {
        public AddFacilityViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Facility name is required.")
                .MaximumLength(100).WithMessage("Facility name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.IconURL)
                .MaximumLength(200).WithMessage("Icon URL cannot exceed 200 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Icon URL must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.IconURL));
            RuleForEach(x => x.RoomIds)
                .NotEmpty().WithMessage("Room ID cannot be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Each Room ID must be a valid GUID.");
        }
    }
}
