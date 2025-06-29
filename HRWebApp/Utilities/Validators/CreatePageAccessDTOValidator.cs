using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class CreatePageAccessDTOValidator : AbstractValidator<CreatePageAccessDTO>
    {
        public CreatePageAccessDTOValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.PageName).NotEmpty().MaximumLength(100);
        }
    }
}
