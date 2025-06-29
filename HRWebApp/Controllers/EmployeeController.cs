using HRWebApp.DTOs;
using HRWebApp.Services.Interfaces;
using HRWebApp.Utilities.Extensions;
using HRWebApp.Utilities.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRWebApp.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;

        public EmployeeController(
            IEmployeeService employeeService,
            IUserService userService)
        {
            _employeeService = employeeService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = User.GetUserId();
            var employee = await _employeeService.GetEmployeeByUserIdAsync(userId);
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.GetUserId();
            var profile = await _userService.GetUserProfileAsync(userId);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileDTO model)
        {
            var validator = new ProfileDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            var userId = User.GetUserId();
            await _userService.UpdateUserProfileAsync(userId, model);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var validator = new ChangePasswordDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            var userId = User.GetUserId();
            await _userService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public async Task<IActionResult> SalarySlips()
        {
            var userId = User.GetUserId();
            var employee = await _employeeService.GetEmployeeByUserIdAsync(userId);
            if (employee == null)
            {
                return NotFound();
            }

            var salarySlips = new List<SalarySlipDTO>
            {
                new SalarySlipDTO
                {
                    MonthYear = "January 2023",
                    BasicSalary = 30000,
                    TotalAllowances = 8000,
                    TotalDeductions = 4000,
                    NetSalary = 34000,
                    DownloadLink = "#"
                },
                new SalarySlipDTO
                {
                    MonthYear = "February 2023",
                    BasicSalary = 30000,
                    TotalAllowances = 8000,
                    TotalDeductions = 4000,
                    NetSalary = 34000,
                    DownloadLink = "#"
                }
            };

            return View(salarySlips);
        }
    }
}