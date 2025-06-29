using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class CreateDepartmentDTOValidator : AbstractValidator<CreateDepartmentDTO>
    {
        public CreateDepartmentDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
