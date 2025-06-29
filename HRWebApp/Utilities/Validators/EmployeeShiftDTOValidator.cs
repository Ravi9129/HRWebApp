using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class EmployeeShiftDTOValidator : AbstractValidator<EmployeeShiftDTO>
    {
        public EmployeeShiftDTOValidator()
        {
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty()
                .Must((model, endTime) => endTime > model.StartTime)
                .WithMessage("End time must be after start time");
            RuleFor(x => x.WeeklyOffDay).NotEmpty().MaximumLength(20);
        }
    }
}
