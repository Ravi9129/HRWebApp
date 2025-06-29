using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class EmployeeBenefitDTOValidator : AbstractValidator<EmployeeBenefitDTO>
    {
        public EmployeeBenefitDTOValidator()
        {
            RuleFor(x => x.BenefitType).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.StartDate).NotEmpty();
        }
    }
}
