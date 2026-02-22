using FluentValidation;
using Hotel.Presentation.ViewModels.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.Facilities
{
    public class UpdateFacilityViewModelValidator:AbstractValidator<UpdateFacilityViewModel>
    {
        public UpdateFacilityViewModelValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Facility name is required.")
                .MaximumLength(100).WithMessage("Facility name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.IconURL)
                .MaximumLength(200).WithMessage("Icon URL must not exceed 200 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Icon URL must be a valid URL.");
        }       
    }
}
