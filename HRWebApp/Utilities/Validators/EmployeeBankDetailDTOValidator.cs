using FluentValidation;
using HRWebApp.DTOs;

namespace HRWebApp.Utilities.Validators
{
    public class EmployeeBankDetailDTOValidator : AbstractValidator<EmployeeBankDetailDTO>
    {
        public EmployeeBankDetailDTOValidator()
        {
            RuleFor(x => x.BankName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.AccountNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.IFSCode).NotEmpty().MaximumLength(11);
            RuleFor(x => x.BranchName).NotEmpty().MaximumLength(100);
        }
    }
}
