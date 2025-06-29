using HRWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRWebApp.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IReportService _reportService;

        public ManagerController(
            IEmployeeService employeeService,
            IDepartmentService departmentService,
            IReportService reportService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalEmployees = await _employeeService.GetActiveEmployeeCountAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
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
    }
}
