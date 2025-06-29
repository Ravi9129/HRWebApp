using HRWebApp.DTOs;
using HRWebApp.Services.Interfaces;
using HRWebApp.Utilities.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRWebApp.Controllers
{
    [Authorize(Roles = "HR")]
    public class HRController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public HRController(
            IEmployeeService employeeService,
            IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalEmployees = await _employeeService.GetActiveEmployeeCountAsync();
            ViewBag.TotalDepartments = await _departmentService.GetDepartmentCountAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO model)
        {
            var validator = new CreateEmployeeDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _employeeService.CreateEmployeeAsync(model);
            TempData["SuccessMessage"] = "Employee created successfully!";
            return RedirectToAction(nameof(Employees));
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new UpdateEmployeeDTO
            {
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DepartmentId = employee.DepartmentId,
                RoleId = employee.RoleId,
                IsActive = employee.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, UpdateEmployeeDTO model)
        {
            var validator = new UpdateEmployeeDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _employeeService.UpdateEmployeeAsync(id, model);
            TempData["SuccessMessage"] = "Employee updated successfully!";
            return RedirectToAction(nameof(Employees));
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO model)
        {
            var validator = new CreateDepartmentDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _departmentService.CreateDepartmentAsync(model);
            TempData["SuccessMessage"] = "Department created successfully!";
            return RedirectToAction(nameof(Departments));
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var model = new UpdateDepartmentDTO
            {
                Name = department.Name,
                Description = department.Description,
                ManagerId = department.ManagerId,
                IsActive = department.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(int id, UpdateDepartmentDTO model)
        {
            var validator = new UpdateDepartmentDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _departmentService.UpdateDepartmentAsync(id, model);
            TempData["SuccessMessage"] = "Department updated successfully!";
            return RedirectToAction(nameof(Departments));
        }
    }
}