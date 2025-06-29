using System.Globalization;
using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;
using OfficeOpenXml;

namespace HRWebApp.Services
{
    public class ReportService : IReportService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReportService(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeReportDTO>> GenerateEmployeeReportAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeReportDTO>>(employees);
        }

        public async Task<IEnumerable<DepartmentReportDTO>> GenerateDepartmentReportAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return _mapper.Map<IEnumerable<DepartmentReportDTO>>(departments);
        }

        public async Task<IEnumerable<SalaryReportDTO>> GenerateSalaryReportAsync(int? month, int? year)
        {
            // In a real application, this would fetch actual salary data
            // For demo purposes, we'll generate some dummy data
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            var salaryReports = employees.Select(e => new SalaryReportDTO
            {
                EmployeeId = e.EmployeeId,
                FullName = $"{e.FirstName} {e.LastName}",
                Department = e.Department,
                BasicSalary = 30000 + new Random().Next(0, 20000),
                TotalAllowances = 5000 + new Random().Next(0, 5000),
                TotalDeductions = 2000 + new Random().Next(0, 3000),
                MonthYear = $"{month ?? DateTime.Now.Month}/{year ?? DateTime.Now.Year}"
            }).ToList();

            salaryReports.ForEach(s => s.NetSalary = s.BasicSalary + s.TotalAllowances - s.TotalDeductions);

            return salaryReports;
        }

        public async Task<byte[]> ExportEmployeeReportToExcelAsync()
        {
            var employees = await GenerateEmployeeReportAsync();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Employees");

            // Add headers
            worksheet.Cells[1, 1].Value = "Employee ID";
            worksheet.Cells[1, 2].Value = "Full Name";
            worksheet.Cells[1, 3].Value = "Department";
            worksheet.Cells[1, 4].Value = "Role";
            worksheet.Cells[1, 5].Value = "Status";
            worksheet.Cells[1, 6].Value = "Joining Date";

            // Add data
            var row = 2;
            foreach (var employee in employees)
            {
                worksheet.Cells[row, 1].Value = employee.EmployeeId;
                worksheet.Cells[row, 2].Value = employee.FullName;
                worksheet.Cells[row, 3].Value = employee.Department;
                worksheet.Cells[row, 4].Value = employee.Role;
                worksheet.Cells[row, 5].Value = employee.Status;
                worksheet.Cells[row, 6].Value = employee.JoiningDate.ToString("d", CultureInfo.InvariantCulture);
                row++;
            }

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }

        public async Task<byte[]> ExportDepartmentReportToExcelAsync()
        {
            var departments = await GenerateDepartmentReportAsync();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Departments");

            // Add headers
            worksheet.Cells[1, 1].Value = "Department Name";
            worksheet.Cells[1, 2].Value = "Employee Count";
            worksheet.Cells[1, 3].Value = "Manager";
            worksheet.Cells[1, 4].Value = "Status";

            // Add data
            var row = 2;
            foreach (var department in departments)
            {
                worksheet.Cells[row, 1].Value = department.DepartmentName;
                worksheet.Cells[row, 2].Value = department.EmployeeCount;
                worksheet.Cells[row, 3].Value = department.ManagerName ?? "N/A";
                worksheet.Cells[row, 4].Value = department.Status;
                row++;
            }

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }

        public async Task<byte[]> ExportSalaryReportToExcelAsync(int? month, int? year)
        {
            var salaries = await GenerateSalaryReportAsync(month, year);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Salaries");

            // Add headers
            worksheet.Cells[1, 1].Value = "Employee ID";
            worksheet.Cells[1, 2].Value = "Full Name";
            worksheet.Cells[1, 3].Value = "Department";
            worksheet.Cells[1, 4].Value = "Basic Salary";
            worksheet.Cells[1, 5].Value = "Total Allowances";
            worksheet.Cells[1, 6].Value = "Total Deductions";
            worksheet.Cells[1, 7].Value = "Net Salary";
            worksheet.Cells[1, 8].Value = "Month/Year";

            // Add data
            var row = 2;
            foreach (var salary in salaries)
            {
                worksheet.Cells[row, 1].Value = salary.EmployeeId;
                worksheet.Cells[row, 2].Value = salary.FullName;
                worksheet.Cells[row, 3].Value = salary.Department;
                worksheet.Cells[row, 4].Value = salary.BasicSalary;
                worksheet.Cells[row, 5].Value = salary.TotalAllowances;
                worksheet.Cells[row, 6].Value = salary.TotalDeductions;
                worksheet.Cells[row, 7].Value = salary.NetSalary;
                worksheet.Cells[row, 8].Value = salary.MonthYear;
                row++;
            }

            // Format currency
            using (var range = worksheet.Cells[2, 4, row - 1, 7])
            {
                range.Style.Numberformat.Format = "#,##0.00";
            }

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}
