using HRWebApp.DTOs;
using HRWebApp.Services.Interfaces;
using HRWebApp.Utilities.Extensions;
using HRWebApp.Utilities.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IProductService _productService;
        private readonly IRoleService _roleService;
        private readonly IPageAccessService _pageAccessService;
        private readonly IReportService _reportService;

        public AdminController(
            IEmployeeService employeeService,
            IDepartmentService departmentService,
            IProductService productService,
            IRoleService roleService,
            IPageAccessService pageAccessService,
            IReportService reportService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _productService = productService;
            _roleService = roleService;
            _pageAccessService = pageAccessService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalEmployees = await _employeeService.GetActiveEmployeeCountAsync();
            ViewBag.TotalDepartments = await _departmentService.GetDepartmentCountAsync();
            ViewBag.TotalProducts = await _productService.GetProductCountAsync();
            return View();
        }

        #region Employee Actions

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            TempData["SuccessMessage"] = "Employee deleted successfully!";
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
        public async Task<IActionResult> EmployeeShift(int id)
        {
            var shift = await _employeeService.GetEmployeeShiftAsync(id);
            if (shift == null)
            {
                shift = new EmployeeShiftDTO
                {
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(18),
                    WeeklyOffDay = "Sunday",
                    HasLunchBreak = true,
                    LunchBreakStart = TimeSpan.FromHours(13),
                    LunchBreakEnd = TimeSpan.FromHours(14)
                };
            }

            return View(shift);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeShift(int id, EmployeeShiftDTO model)
        {
            var validator = new EmployeeShiftDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _employeeService.UpdateEmployeeShiftAsync(id, model);
            TempData["SuccessMessage"] = "Employee shift updated successfully!";
            return RedirectToAction(nameof(EmployeeDetails), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeBankDetails(int id)
        {
            var bankDetails = await _employeeService.GetEmployeeBankDetailAsync(id);
            if (bankDetails == null)
            {
                bankDetails = new EmployeeBankDetailDTO();
            }

            return View(bankDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeBankDetails(int id, EmployeeBankDetailDTO model)
        {
            var validator = new EmployeeBankDetailDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _employeeService.UpdateEmployeeBankDetailAsync(id, model);
            TempData["SuccessMessage"] = "Employee bank details updated successfully!";
            return RedirectToAction(nameof(EmployeeDetails), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeBenefits(int id)
        {
            var benefits = await _employeeService.GetEmployeeBenefitsAsync(id);
            return View(benefits);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployeeBenefit(int id, EmployeeBenefitDTO model)
        {
            var validator = new EmployeeBenefitDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return RedirectToAction(nameof(EmployeeBenefits), new { id });
            }

            await _employeeService.AddEmployeeBenefitAsync(id, model);
            TempData["SuccessMessage"] = "Benefit added successfully!";
            return RedirectToAction(nameof(EmployeeBenefits), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveEmployeeBenefit(int id, int benefitId)
        {
            await _employeeService.RemoveEmployeeBenefitAsync(benefitId);
            TempData["SuccessMessage"] = "Benefit removed successfully!";
            return RedirectToAction(nameof(EmployeeBenefits), new { id });
        }

        #endregion

        #region Department Actions

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            TempData["SuccessMessage"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Departments));
        }

        #endregion

        #region Product Actions

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductDTO model)
        {
            var validator = new CreateProductDTOValidator();
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
            await _productService.CreateProductAsync(model, userId);
            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction(nameof(Products));
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new UpdateProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, UpdateProductDTO model)
        {
            var validator = new UpdateProductDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _productService.UpdateProductAsync(id, model);
            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDeleteProduct(int id)
        {
            await _productService.SoftDeleteProductAsync(id);
            TempData["SuccessMessage"] = "Product moved to recycle bin!";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreProduct(int id)
        {
            await _productService.RestoreProductAsync(id);
            TempData["SuccessMessage"] = "Product restored successfully!";
            return RedirectToAction(nameof(Products));
        }

        #endregion

        #region Role Actions

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            var validator = new CreateRoleDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _roleService.CreateRoleAsync(model);
            TempData["SuccessMessage"] = "Role created successfully!";
            return RedirectToAction(nameof(Roles));
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(string id, string name)
        {
            await _roleService.UpdateRoleAsync(id, name);
            TempData["SuccessMessage"] = "Role updated successfully!";
            return RedirectToAction(nameof(Roles));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _roleService.DeleteRoleAsync(id);
            TempData["SuccessMessage"] = "Role deleted successfully!";
            return RedirectToAction(nameof(Roles));
        }

        #endregion

        #region Page Access Actions

        [HttpGet]
        public async Task<IActionResult> PageAccess()
        {
            var pageAccesses = await _pageAccessService.GetAllPageAccessesAsync();
            return View(pageAccesses);
        }

        [HttpGet]
        public IActionResult CreatePageAccess()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePageAccess(CreatePageAccessDTO model)
        {
            var validator = new CreatePageAccessDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _pageAccessService.CreatePageAccessAsync(model);
            TempData["SuccessMessage"] = "Page access created successfully!";
            return RedirectToAction(nameof(PageAccess));
        }

        [HttpGet]
        public async Task<IActionResult> EditPageAccess(int id)
        {
            var pageAccess = await _pageAccessService.GetPageAccessByIdAsync(id);
            if (pageAccess == null)
            {
                return NotFound();
            }

            var model = new UpdatePageAccessDTO
            {
                CanView = pageAccess.CanView,
                CanCreate = pageAccess.CanCreate,
                CanEdit = pageAccess.CanEdit,
                CanDelete = pageAccess.CanDelete
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPageAccess(int id, UpdatePageAccessDTO model)
        {
            var validator = new UpdatePageAccessDTOValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            await _pageAccessService.UpdatePageAccessAsync(id, model);
            TempData["SuccessMessage"] = "Page access updated successfully!";
            return RedirectToAction(nameof(PageAccess));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePageAccess(int id)
        {
            await _pageAccessService.DeletePageAccessAsync(id);
            TempData["SuccessMessage"] = "Page access deleted successfully!";
            return RedirectToAction(nameof(PageAccess));
        }

        #endregion

        #region Report Actions

        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeReport()
        {
            var report = await _reportService.GenerateEmployeeReportAsync();
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentReport()
        {
            var report = await _reportService.GenerateDepartmentReportAsync();
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> SalaryReport(int? month, int? year)
        {
            var report = await _reportService.GenerateSalaryReportAsync(month, year);
            ViewBag.Month = month;
            ViewBag.Year = year;
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> ExportEmployeeReportToExcel()
        {
            var fileContents = await _reportService.ExportEmployeeReportToExcelAsync();
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportDepartmentReportToExcel()
        {
            var fileContents = await _reportService.ExportDepartmentReportToExcelAsync();
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Departments.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportSalaryReportToExcel(int? month, int? year)
        {
            var fileContents = await _reportService.ExportSalaryReportToExcelAsync(month, year);
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Salaries.xlsx");
        }

        #endregion
    }
}