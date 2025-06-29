using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class UpdateEmployeeDTOValidator : AbstractValidator<UpdateEmployeeDTO>
    {
        public UpdateEmployeeDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.MiddleName).MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DepartmentId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.RoleId).NotEmpty();
        }
    }
}
