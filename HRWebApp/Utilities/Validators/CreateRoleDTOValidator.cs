using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
    {
        public CreateRoleDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
