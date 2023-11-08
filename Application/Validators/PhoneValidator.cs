using Domain;
using FluentValidation;

namespace Application
{
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(phone => phone.PhoneNumber)
                .MinimumLength(4).WithMessage("Phone number must be at least 4 digits long.")
                .MaximumLength(8).WithMessage("Phone number must not exceed 8 digits.");
        }
    }
}