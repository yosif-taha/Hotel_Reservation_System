using FluentValidation;
using Hotel.Presentation.ViewModels.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Validations.FeedBack
{
    public class AddStaffResponseViewModelValidator : AbstractValidator<AddStaffResponseViewModel>
    {
    public AddStaffResponseViewModelValidator() {

        RuleFor(x => x.StaffResponse)
            .NotEmpty().WithMessage("Staff response is required.")
            .MaximumLength(1000).WithMessage("Staff response cannot exceed 1000 characters.");
        }
    }
}
