using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class UpdatePageAccessDTOValidator : AbstractValidator<UpdatePageAccessDTO>
    {
        public UpdatePageAccessDTOValidator()
        {
            RuleFor(x => x.CanView).NotNull();
            RuleFor(x => x.CanCreate).NotNull();
            RuleFor(x => x.CanEdit).NotNull();
            RuleFor(x => x.CanDelete).NotNull();
        }
    }
}
